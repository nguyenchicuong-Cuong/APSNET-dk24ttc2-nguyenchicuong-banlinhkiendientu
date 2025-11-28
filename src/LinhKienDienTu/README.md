# 🔌 Linh Kiện Điện Tử Premium (ElectroShop)

> **Phiên bản**: 1.0.0
> **Trạng thái**: Đã hoàn thành (Production Ready)

Dự án website thương mại điện tử chuyên kinh doanh linh kiện điện tử, bo mạch, cảm biến và thiết bị IoT. Hệ thống được xây dựng trên nền tảng **ASP.NET Core 9.0 MVC** mạnh mẽ, kết hợp với giao diện **Glassmorphism** (kính mờ) hiện đại, mang lại trải nghiệm người dùng độc đáo và cao cấp.

---

## 📑 Mục Lục

1.  [Công Nghệ & Kiến Trúc](#-công-nghệ--kiến-trúc)
2.  [Cơ Sở Dữ Liệu](#-cơ-sở-dữ-liệu)
3.  [Chi Tiết Tính Năng](#-chi-tiết-tính-năng)
    *   [Phân Hệ Người Dùng (Client)](#1-phân-hệ-người-dùng-client)
    *   [Phân Hệ Quản Trị (Admin)](#2-phân-hệ-quản-trị-admin)
4.  [Hướng Dẫn Cài Đặt & Triển Khai](#-hướng-dẫn-cài-đặt--triển-khai)
5.  [Hướng Dẫn Sử Dụng](#-hướng-dẫn-sử-dụng)
6.  [Cấu Trúc Dự Án](#-cấu-trúc-dự-án)

---

## �️ Công Nghệ & Kiến Trúc

### Backend Stack
*   **Framework**: ASP.NET Core 9.0 (Mô hình MVC - Model-View-Controller).
*   **Ngôn ngữ**: C# 12.0.
*   **Database Access**: Entity Framework Core 9.0 (Code-First Approach).
*   **Database**: Microsoft SQL Server (LocalDB cho Development).
*   **Identity & Security**:
    *   ASP.NET Core Identity: Quản lý xác thực (Authentication) và phân quyền (Authorization).
    *   Password Hashing: PBKDF2 with HMAC-SHA256.
    *   CSRF Protection: Antiforgery Tokens.
*   **Dependency Injection**: Built-in container của .NET Core.

### Frontend Stack
*   **View Engine**: Razor Views (.cshtml) với Tag Helpers.
*   **CSS Framework**: Bootstrap 5.3 (Customize).
*   **Design System**: **Glassmorphism UI**
    *   Sử dụng `backdrop-filter: blur()` cho hiệu ứng kính mờ.
    *   Gradient Background động (Animated Gradient).
    *   Dark Mode mặc định để làm nổi bật sản phẩm công nghệ.
*   **Icons**: FontAwesome 6.4 Free.
*   **JavaScript**: Vanilla JS (ES6+) cho các tương tác UI, jQuery (hỗ trợ Bootstrap).

---

## 🗄️ Cơ Sở Dữ Liệu

Hệ thống sử dụng SQL Server với các bảng chính sau:

1.  **AspNetUsers**: Lưu thông tin tài khoản (Admin & Customer).
2.  **Categories**: Danh mục sản phẩm (Hỗ trợ cấu trúc cây cha-con).
    *   `Id`, `Name`, `Description`, `ParentId`.
3.  **Products**: Sản phẩm linh kiện.
    *   `Id`, `Name`, `Price`, `BulkPrice` (Giá sỉ), `StockQuantity`, `ImageUrl`, `DatasheetUrl`, `Specifications` (JSON/Text), `CategoryId`.
4.  **Orders**: Đơn hàng.
    *   `Id`, `UserId`, `OrderDate`, `TotalAmount`, `Status` (Pending, Processing, Shipping, Delivered, Cancelled), `ShippingAddress`, `PaymentMethod`.
5.  **OrderItems**: Chi tiết đơn hàng.
    *   `Id`, `OrderId`, `ProductId`, `Quantity`, `UnitPrice`.
6.  **ProductReviews**: Đánh giá sản phẩm.
    *   `Id`, `ProductId`, `UserId`, `Rating` (1-5 sao), `Comment`, `ReviewDate`.

---

## ✨ Chi Tiết Tính Năng

### 1. Phân Hệ Người Dùng (Client)

#### 🛒 Mua Sắm & Sản Phẩm
*   **Trang Chủ**: Hiển thị sản phẩm nổi bật, sản phẩm mới nhất với hiệu ứng card kính.
*   **Danh Sách Sản Phẩm**:
    *   Hiển thị dạng lưới (Grid).
    *   Phân trang (Pagination).
    *   Hiển thị giá và nút "Thêm vào giỏ" nhanh.
*   **Chi Tiết Sản Phẩm**:
    *   Hình ảnh lớn, rõ nét.
    *   Thông tin chi tiết: Mã linh kiện, Nhà sản xuất, Tồn kho.
    *   **Giá sỉ**: Hiển thị giá ưu đãi khi mua số lượng lớn (>100 cái).
    *   **Datasheet**: Link tải tài liệu kỹ thuật PDF trực tiếp.
    *   **Đánh giá**: Xem và viết đánh giá (sao + bình luận) - Yêu cầu đăng nhập.

#### 🛍️ Giỏ Hàng & Thanh Toán
*   **Giỏ Hàng Thông Minh**:
    *   Lưu trữ trong Session (không mất khi refresh trang).
    *   Tự động cập nhật tổng tiền khi thay đổi số lượng.
    *   Xóa sản phẩm khỏi giỏ.
*   **Checkout (Thanh Toán)**:
    *   Form nhập thông tin giao hàng (Tự động điền nếu đã đăng nhập).
    *   Chọn phương thức thanh toán (COD, Chuyển khoản).
    *   Xác nhận đơn hàng và gửi email (mô phỏng).

#### 👤 Tài Khoản
*   **Đăng Ký/Đăng Nhập**: Giao diện kính mờ đẹp mắt, validate dữ liệu chặt chẽ.
*   **Quên Mật Khẩu**: Quy trình gửi email (mô phỏng) để reset mật khẩu.
*   **Quản Lý Hồ Sơ**: Cập nhật thông tin cá nhân.
*   **Lịch Sử Đơn Hàng**:
    *   Danh sách đơn hàng đã đặt.
    *   Trạng thái xử lý (Màu sắc trực quan: Vàng - Chờ, Xanh - Đã giao...).
    *   Xem lại chi tiết từng đơn hàng.

---

### 2. Phân Hệ Quản Trị (Admin)
*Truy cập: `/Admin` (Yêu cầu quyền Admin)*

#### 📊 Dashboard (Bảng Điều Khiển)
*   **Thống Kê Real-time**:
    *   Doanh thu hôm nay.
    *   Số đơn hàng mới chờ xử lý.
    *   Số lượng khách hàng mới.
    *   Số sản phẩm sắp hết hàng (Low stock alert).
*   **Biểu Đồ & Danh Sách**:
    *   Top 5 đơn hàng gần nhất.
    *   Top sản phẩm bán chạy.

#### 📦 Quản Lý Sản Phẩm (Product Manager)
*   **CRUD Đầy Đủ**: Thêm, Xem, Sửa, Xóa sản phẩm.
*   **Upload Ảnh**: Hỗ trợ nhập URL ảnh (hoặc mở rộng upload file).
*   **Thông Số Kỹ Thuật**: Nhập liệu chi tiết cho linh kiện điện tử.

#### 📂 Quản Lý Danh Mục (Category Manager)
*   **Cấu Trúc Cây**: Quản lý danh mục cha và danh mục con.
*   **An Toàn Dữ Liệu**: Chặn xóa danh mục nếu đang chứa sản phẩm để đảm bảo toàn vẹn dữ liệu.

#### 🧾 Quản Lý Đơn Hàng (Order Manager)
*   **Quy Trình Xử Lý**:
    1.  Xem đơn hàng mới (Pending).
    2.  Duyệt đơn hàng (Processing).
    3.  Giao hàng (Shipping).
    4.  Hoàn tất (Delivered) hoặc Hủy (Cancelled).
*   **Chi Tiết**: Xem rõ người mua, địa chỉ, danh sách sản phẩm và tổng tiền.

#### 👥 Quản Lý Khách Hàng (Customer Manager)
*   Xem danh sách người dùng.
*   Thống kê tổng tiền khách đã mua (Lifetime Value).

#### 📈 Báo Cáo (Reports)
*   Báo cáo doanh thu chi tiết.
*   Phân tích sản phẩm bán chạy / tồn kho lâu.

---

## � Hướng Dẫn Cài Đặt & Triển Khai

### Yêu Cầu Hệ Thống
*   .NET SDK 9.0 trở lên.
*   Visual Studio 2022 hoặc VS Code.
*   SQL Server (LocalDB hoặc bản Standard/Enterprise).

### Các Bước Cài Đặt

1.  **Clone Repository**:
    ```bash
    git clone https://github.com/your-username/LinhKienDienTu.git
    cd LinhKienDienTu
    ```

2.  **Cấu Hình Database**:
    *   Mở file `appsettings.json`.
    *   Chỉnh sửa chuỗi kết nối `DefaultConnection` nếu cần (Mặc định dùng LocalDB).

3.  **Khởi Tạo Database**:
    *   Mở terminal tại thư mục dự án.
    *   Chạy lệnh Migration:
    ```bash
    dotnet ef database update
    ```
    *   *Hệ thống sẽ tự động tạo Database và các bảng.*

4.  **Seed Data (Dữ Liệu Mẫu)**:
    *   Khi chạy lần đầu, `DbInitializer` sẽ tự động tạo:
        *   1 Admin, 1 Customer.
        *   5 Danh mục mẫu.
        *   10 Sản phẩm linh kiện mẫu.
        *   2 Đơn hàng mẫu.

5.  **Chạy Ứng Dụng**:
    ```bash
    dotnet run
    ```
    *   Truy cập: `http://localhost:5171`

---

## � Hướng Dẫn Sử Dụng

### 1. Đăng Nhập Hệ Thống
Sử dụng tài khoản mặc định để trải nghiệm:

| Vai Trò | Email | Mật Khẩu | Quyền Hạn |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@example.com` | `Admin@123` | Truy cập toàn bộ Admin Panel, Quản lý hệ thống. |
| **Customer** | `customer@example.com` | `Customer@123` | Mua hàng, Xem lịch sử, Đánh giá sản phẩm. |

### 2. Quy Trình Mua Hàng (User)
1.  Đăng nhập tài khoản Customer.
2.  Duyệt sản phẩm ở trang chủ hoặc trang Sản phẩm.
3.  Nhấn "Thêm vào giỏ" hoặc xem chi tiết rồi thêm.
4.  Vào Giỏ hàng -> Nhấn "Thanh toán".
5.  Điền thông tin giao hàng -> "Đặt hàng".
6.  Vào "Tài khoản" -> "Đơn hàng của tôi" để theo dõi.

### 3. Quy Trình Xử Lý Đơn (Admin)
1.  Đăng nhập tài khoản Admin.
2.  Vào "Admin Panel" (Menu dropdown trên navbar).
3.  Chọn "Quản lý đơn hàng".
4.  Chọn "Chi tiết" đơn hàng mới nhất.
5.  Đổi trạng thái từ "Chờ xử lý" sang "Đang giao" -> Lưu.

---

## 📂 Cấu Trúc Dự Án Chi Tiết

```text
LinhKienDienTu/
├── Areas/Admin/                # PHÂN HỆ ADMIN
│   ├── Controllers/            # Logic xử lý Admin
│   │   ├── CategoryManager...  # Quản lý danh mục
│   │   ├── ProductManager...   # Quản lý sản phẩm
│   │   ├── OrderManager...     # Quản lý đơn hàng
│   │   ├── Reports...          # Báo cáo thống kê
│   │   └── HomeController.cs   # Dashboard
│   └── Views/                  # Giao diện Admin (Razor)
│       ├── Shared/_Layout.cshtml # Layout riêng cho Admin
│       └── ...
├── Controllers/                # PHÂN HỆ USER
│   ├── AccountController.cs    # Đăng nhập/Đăng ký/Profile
│   ├── CartController.cs       # Giỏ hàng (Session)
│   ├── CheckoutController.cs   # Thanh toán
│   ├── OrderController.cs      # Lịch sử đơn hàng user
│   └── ProductController.cs    # Hiển thị sản phẩm
├── Data/                       # DATABASE
│   ├── ApplicationDbContext.cs # EF Core Context
│   └── DbInitializer.cs        # Seed Data (Dữ liệu mẫu)
├── Models/                     # ENTITIES (Database Models)
│   ├── Product.cs
│   ├── Category.cs
│   ├── Order.cs
│   └── ...
├── ViewModels/                 # DTOs (Data Transfer Objects)
│   ├── LoginViewModel.cs
│   ├── CheckoutViewModel.cs
│   └── ...
├── Views/                      # GIAO DIỆN USER
│   ├── Shared/_Layout.cshtml   # Layout chính (Glassmorphism)
│   └── ...
├── wwwroot/                    # STATIC FILES
│   ├── css/site.css            # Custom CSS (Glass effects)
│   └── js/                     # Scripts
├── Program.cs                  # App Configuration & DI
└── appsettings.json            # Config (Connection String)
```

---
*© 2025 Linh Kiện Điện Tử Premium.*
