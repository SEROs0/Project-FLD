> ⚠️ โปรเจคนี้ยังอยู่ในขั้นตอน **Development** — Branch `Developer` เป็น branch หลักสำหรับการพัฒนาซอร์สโค้ด ก่อนที่จะทำการ Merge หรือ Pull Request ขึ้นไปยัง branch `master`

# ระบบจัดการคลังสินค้า (ERP System)
ระบบบริหารจัดการคำสั่งซื้อและคลังสินค้าสำหรับโรงงานแพ็คเกจจิ้ง พัฒนาด้วย VB.NET Windows Forms เชื่อมต่อกับ SQL Server

---

## ภาพรวมระบบ

ระบบนี้พัฒนาขึ้นเพื่อแก้ปัญหาการติดตาม Order ด้วยมือหรือ Excel ซึ่งทำให้เกิดความผิดพลาดบ่อยครั้ง โดยระบบจะครอบคลุม Flow การทำงานตั้งแต่รับ Order จากลูกค้า ตรวจสอบ Stock สร้าง Job และส่ง PR ให้ฝ่ายจัดซื้อในกรณีที่สินค้าไม่เพียงพอ


## หน้าจอหลักของระบบ

### 1. หน้า Login
- ตรวจสอบ Username และ Password จาก Database
- แสดงข้อความแจ้งเตือนเมื่อกรอกข้อมูลไม่ถูกต้อง
- เข้าสู่ระบบสำเร็จ → เด้งไปหน้า Store อัตโนมัติ

### 2. หน้า Store (คลังสินค้า)
- แสดงภาพรวม Stock สินค้าทั้งหมด
- แสดงมูลค่าสินค้าในคลังรวม
- ค้นหาสินค้าตาม Category ได้
- แสดงจำนวน Stock แบบ Real-time

### 3. หน้า Orders
- สร้าง Order ใหม่จากลูกค้า
- เลือกสินค้าได้หลายรายการต่อ Order เดียว
- คำนวณยอดรวมอัตโนมัติ
- ค้นหาและกรอง Order ตาม Status
- ดูรายละเอียด Order และยกเลิก Order ได้
- Summary Cards แสดงจำนวน Order ทั้งหมด / PENDING / COMPLETE / ยอดรวม

### 4. หน้า Jobs
- ระบบสร้าง Job อัตโนมัติเมื่อ Order ถูกสร้าง
- ตรวจสอบ Stock แบบ Real-time ว่าเพียงพอหรือไม่
- แสดงสีเขียว = Stock พอ / สีแดง = Stock ไม่พอ
- กดปุ่ม Complete เพื่อหักสต็อกและเปลี่ยน Status เป็น COMPLETE
- กดปุ่ม ส่ง PR เมื่อ Stock ไม่เพียงพอ
- Summary Cards แสดงจำนวน Job / Stock พอ / Stock ไม่พอ

### 5. หน้า PR/PO (ใบขอซื้อ)
- แสดงรายการ PR ทั้งหมดที่ส่งมาจากระบบ Job
- อนุมัติ (APPROVED) หรือปฏิเสธ (REJECTED) PR แต่ละรายการได้
- ดูรายละเอียด PR พร้อมหมายเหตุจากผู้ขอ
- ค้นหากรอง PR ตาม Status
- Summary Cards แสดงจำนวน PR ทั้งหมด / รออนุมัติ / อนุมัติแล้ว

---

## เทคโนโลยีที่ใช้

| ส่วน | เทคโนโลยี |
|------|-----------|
| ภาษา | Visual Basic .NET |
| Framework | .NET 10.0 |
| UI | Windows Forms + Guna UI2 |
| Database | SQL Server 2025 Express |
| IDE | Visual Studio 2026 |
| DB Tool | SQL Server Management Studio (SSMS) 22 |

---

## โครงสร้าง Database

```
customers
├── cust_id (PK)
├── cust_name
└── phone

orders
├── order_id (PK)
├── order_code (UNIQUE)
├── cust_id (FK → customers)
├── amount
├── status
└── order_date

order_items
├── item_id (PK)
├── order_id (FK → orders)
├── product_id (FK → products)
├── qty
└── item_status

products
├── product_id (PK)
├── product_name
├── price
├── category
└── stock_quantity

jobs
├── job_id (PK)
├── job_code (UNIQUE)
├── order_id (FK → orders)
├── job_status
└── created_date

purchase_requests
├── pr_id (PK)
├── pr_code (UNIQUE)
├── order_id (FK → orders)
├── job_id (FK → jobs)
├── product_id (FK → products)
├── qty_needed
├── note
├── pr_status
└── created_date
```

## วิธีติดตั้ง

### ความต้องการของระบบ
- SQL Server 2025 Express
- Visual Studio 2026 (สำหรับ Developer)

### ขั้นตอนการติดตั้ง

**1. Clone Repository**
```bash
[git clone https://github.com/username/erp-system.git](https://github.com/SEROs0/Project-FLD.git)
```

**2. สร้าง Database**

```sql
-- สร้าง Database
CREATE DATABASE PracticeDB

USE PracticeDB

-- สร้าง Table customers
CREATE TABLE customers (
    cust_id   INT PRIMARY KEY,
    cust_name NVARCHAR(100),
    phone     NVARCHAR(20)
)

-- สร้าง Table products
CREATE TABLE products (
    product_id     INT PRIMARY KEY,
    product_name   NVARCHAR(100),
    price          DECIMAL(10,2),
    category       NVARCHAR(50),
    stock_quantity INT DEFAULT 0
)

-- สร้าง Table orders
CREATE TABLE orders (
    order_id   INT PRIMARY KEY IDENTITY(1,1),
    order_code NVARCHAR(20) UNIQUE,
    cust_id    INT ,
    amount     DECIMAL(10,2),
    status     NVARCHAR(20) DEFAULT 'PENDING',
    order_date DATE DEFAULT GETDATE()
)

-- สร้าง Table order_items
CREATE TABLE order_items (
    item_id     INT PRIMARY KEY IDENTITY(1,1),
    order_id    INT ,
    product_id  INT,
    qty         INT,
    item_status NVARCHAR(20) DEFAULT 'PENDING'
)

-- สร้าง Table jobs
CREATE TABLE jobs (
    job_id       INT PRIMARY KEY IDENTITY(1,1),
    job_code     NVARCHAR(20) UNIQUE,
    order_id     INT ,
    job_status   NVARCHAR(20) DEFAULT 'CHECKING',
    created_date DATETIME DEFAULT GETDATE()
)

-- สร้าง Table purchase_requests
CREATE TABLE purchase_requests (
    pr_id        INT PRIMARY KEY IDENTITY(1,1),
    pr_code      NVARCHAR(20),
    order_id     INT ,
    job_id       INT ,
    product_id   INT ,
    qty_needed   INT,
    note         NVARCHAR(500),
    pr_status    NVARCHAR(20) DEFAULT 'PENDING',
    created_date DATETIME DEFAULT GETDATE()
)
```

**3. แก้ Connection String**

เปิดไฟล์แต่ละ Form แล้วแก้ Connection String ให้ตรงกับ Server ของคุณ

```vb
Dim connString As String = "Server=localhost\SQLEXPRESS;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
```

**4. เปิดโปรเจกต์**

เปิดไฟล์ `learning.slnx` ด้วย Visual Studio แล้วกด Run (F5)

---

## Flow การทำงานของระบบ

```
1. Login เข้าสู่ระบบ
        ↓
2. สร้าง Order รับคำสั่งซื้อจากลูกค้า
        ↓
3. กดปุ่ม JOB → ระบบสร้าง Job อัตโนมัติ
        ↓
4. Job ตรวจสอบ Stock
        ↓
   Stock พอ ──────────────────→ กด Complete
        ↓                              ↓
   Stock ไม่พอ              Order → COMPLETE
        ↓                    Stock ถูกหักออก
5. ส่ง PR ให้ฝ่ายจัดซื้อ
        ↓
6. ฝ่ายจัดซื้อ Approve PR
        ↓
7. Store รับสินค้าเพิ่ม Stock
        ↓
8. กลับไป Complete Job ได้
```
