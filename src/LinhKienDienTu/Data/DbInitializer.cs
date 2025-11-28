using LinhKienDienTu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LinhKienDienTu.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

            // Check if DB has been seeded
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            // 1. Seed Users
            var adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com", EmailConfirmed = true };
            var customerUser = new IdentityUser { UserName = "customer@example.com", Email = "customer@example.com", EmailConfirmed = true };

            if (!context.Users.Any(u => u.UserName == adminUser.UserName))
            {
                userManager.CreateAsync(adminUser, "Admin@123").Wait();
                userManager.CreateAsync(customerUser, "Customer@123").Wait();
            }
            
            // Re-fetch users to get Ids
            var admin = context.Users.FirstOrDefault(u => u.Email == "admin@example.com");
            var customer = context.Users.FirstOrDefault(u => u.Email == "customer@example.com");

            // 2. Seed Categories
            var categories = new Category[]
            {
                new Category{Name="Vi điều khiển", Description="Các loại chip vi điều khiển ARM, AVR, PIC..."},
                new Category{Name="Cảm biến", Description="Cảm biến nhiệt độ, độ ẩm, ánh sáng, gia tốc..."},
                new Category{Name="Linh kiện thụ động", Description="Điện trở, tụ điện, cuộn cảm, biến trở"},
                new Category{Name="Module ứng dụng", Description="Module Relay, Wifi, Bluetooth, GPS"},
                new Category{Name="Dụng cụ", Description="Mỏ hàn, đồng hồ đo, kìm, nhíp"}
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            // 3. Seed Products
            var products = new Product[]
            {
                // Vi điều khiển
                new Product{
                    Name="Arduino Uno R3", 
                    PartNumber="ARD-UNO-R3", 
                    Description="Bo mạch lập trình Arduino Uno R3 chip cắm ATmega328P. Phù hợp cho người mới bắt đầu.", 
                    Price=150000, 
                    BulkPrice=140000,
                    StockQuantity=100, 
                    CategoryId=categories[0].Id, 
                    Manufacturer="Arduino", 
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/3/38/Arduino_Uno_-_R3.jpg",
                    Specifications="Microcontroller: ATmega328P\nOperating Voltage: 5V\nInput Voltage (recommended): 7-12V\nDigital I/O Pins: 14\nPWM Digital I/O Pins: 6\nAnalog Input Pins: 6",
                    DatasheetUrl="https://docs.arduino.cc/resources/datasheets/A000066-datasheet.pdf"
                },
                new Product{
                    Name="ESP8266 NodeMCU", 
                    PartNumber="ESP8266-NODEMCU", 
                    Description="Module Wifi ESP8266 NodeMCU Lua CP2102. Tích hợp Wifi, dễ dàng lập trình IoT.", 
                    Price=85000, 
                    BulkPrice=80000,
                    StockQuantity=200, 
                    CategoryId=categories[0].Id, 
                    Manufacturer="Espressif", 
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Nodemcu_pinout.png/800px-Nodemcu_pinout.png",
                    Specifications="Wi-Fi Module: ESP-12E\nUSB-TTL converter: CP2102\nPower Input: 4.5V ~ 9V (10VMAX), USB-powered",
                    DatasheetUrl="https://www.espressif.com/sites/default/files/documentation/0a-esp8266ex_datasheet_en.pdf"
                },
                new Product{
                    Name="STM32F103C8T6 Blue Pill", 
                    PartNumber="STM32-BLUEPILL", 
                    Description="Kit phát triển STM32F103C8T6 ARM Cortex-M3. Hiệu năng cao, giá rẻ.", 
                    Price=65000, 
                    BulkPrice=60000,
                    StockQuantity=150, 
                    CategoryId=categories[0].Id, 
                    Manufacturer="STMicroelectronics", 
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/STM32F103C8T6_Blue_Pill.jpg/800px-STM32F103C8T6_Blue_Pill.jpg",
                    Specifications="Core: ARM 32-bit Cortex-M3 CPU\n72 MHz maximum frequency\n64 or 128 Kbytes of Flash memory\n20 Kbytes of SRAM",
                    DatasheetUrl="https://www.st.com/resource/en/datasheet/stm32f103c8.pdf"
                },

                // Cảm biến
                new Product{
                    Name="Cảm biến nhiệt độ độ ẩm DHT11", 
                    PartNumber="DHT11", 
                    Description="Cảm biến nhiệt độ và độ ẩm DHT11 giá rẻ, giao tiếp 1 dây.", 
                    Price=25000, 
                    BulkPrice=22000,
                    StockQuantity=500, 
                    CategoryId=categories[1].Id, 
                    Manufacturer="Aosong", 
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/DHT11_Humidity_Sensor.jpg/600px-DHT11_Humidity_Sensor.jpg",
                    Specifications="Humidity Range: 20-90% RH\nHumidity Accuracy: ±5% RH\nTemperature Range: 0-50°C\nTemperature Accuracy: ±2°C",
                    DatasheetUrl="https://www.mouser.com/datasheet/2/758/DHT11-Technical-Data-Sheet-Translated-Version-1143054.pdf"
                },
                new Product{
                    Name="Cảm biến siêu âm HC-SR04", 
                    PartNumber="HC-SR04", 
                    Description="Cảm biến đo khoảng cách bằng sóng siêu âm.", 
                    Price=30000, 
                    BulkPrice=28000,
                    StockQuantity=300, 
                    CategoryId=categories[1].Id, 
                    Manufacturer="Generic", 
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/HC-SR04_Ultrasonic_Sensor.jpg/800px-HC-SR04_Ultrasonic_Sensor.jpg",
                    Specifications="Power Supply :+5V DC\nQuiescent Current : <2mA\nWorking Current: 15mA\nEffectual Angle: <15°\nRanging Distance : 2cm – 400 cm",
                    DatasheetUrl=""
                },

                // Linh kiện thụ động
                new Product{
                    Name="Bộ điện trở 1/4W 30 giá trị", 
                    PartNumber="RES-KIT-30", 
                    Description="Bộ 600 điện trở 1/4W 5% với 30 giá trị thông dụng (10Ω - 1MΩ).", 
                    Price=50000, 
                    BulkPrice=45000,
                    StockQuantity=100, 
                    CategoryId=categories[2].Id, 
                    Manufacturer="Yageo", 
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Resistors_SMD.jpg/800px-Resistors_SMD.jpg", // Placeholder
                    Specifications="Power Rating: 0.25W\nTolerance: 5%\nValues: 10R, 22R, 47R, 100R, 150R, 200R, 220R, 270R, 330R, 470R, 510R, 680R, 1K, 2K, 2.2K, 3.3K, 4.7K, 5.1K, 6.8K, 10K, 20K, 47K, 51K, 68K, 100K, 220K, 300K, 470K, 680K, 1M",
                    DatasheetUrl=""
                },
                 new Product{
                    Name="Tụ hóa 1000uF 25V", 
                    PartNumber="CAP-1000-25", 
                    Description="Tụ điện phân nhôm 1000uF điện áp 25V.", 
                    Price=2000, 
                    BulkPrice=1500,
                    StockQuantity=2000, 
                    CategoryId=categories[2].Id, 
                    Manufacturer="Rubycon", 
                    ImageUrl="",
                    Specifications="Capacitance: 1000uF\nVoltage: 25V\nType: Radial Lead",
                    DatasheetUrl=""
                }
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            // 4. Seed Orders
            if (customer != null)
            {
                var orders = new Order[]
                {
                    new Order
                    {
                        UserId = customer.Id,
                        OrderDate = DateTime.Now.AddDays(-5),
                        CustomerName = "Nguyễn Văn A",
                        PhoneNumber = "0901234567",
                        ShippingAddress = "123 Đường ABC, Quận 1, TP.HCM",
                        Email = "customer@example.com",
                        Status = OrderStatus.Delivered,
                        PaymentMethod = PaymentMethod.COD,
                        IsPaid = true,
                        TotalAmount = 175000,
                        Notes = "Giao giờ hành chính"
                    },
                    new Order
                    {
                        UserId = customer.Id,
                        OrderDate = DateTime.Now.AddDays(-1),
                        CustomerName = "Nguyễn Văn A",
                        PhoneNumber = "0901234567",
                        ShippingAddress = "123 Đường ABC, Quận 1, TP.HCM",
                        Email = "customer@example.com",
                        Status = OrderStatus.Processing,
                        PaymentMethod = PaymentMethod.BankTransfer,
                        IsPaid = true,
                        TotalAmount = 85000,
                        Notes = ""
                    }
                };
                context.Orders.AddRange(orders);
                context.SaveChanges();

                // Order Items
                var orderItems = new OrderItem[]
                {
                    new OrderItem { OrderId = orders[0].Id, ProductId = products[0].Id, Quantity = 1, UnitPrice = 150000 }, // Arduino
                    new OrderItem { OrderId = orders[0].Id, ProductId = products[3].Id, Quantity = 1, UnitPrice = 25000 },  // DHT11
                    new OrderItem { OrderId = orders[1].Id, ProductId = products[1].Id, Quantity = 1, UnitPrice = 85000 }   // ESP8266
                };
                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();
            }

            // 5. Seed Reviews
            if (customer != null)
            {
                var reviews = new ProductReview[]
                {
                    new ProductReview
                    {
                        ProductId = products[0].Id, // Arduino
                        UserId = customer.Id,
                        UserName = "customer@example.com",
                        Rating = 5,
                        Comment = "Sản phẩm chính hãng, chạy rất tốt. Giao hàng nhanh.",
                        ReviewDate = DateTime.Now.AddDays(-4)
                    },
                    new ProductReview
                    {
                        ProductId = products[1].Id, // ESP8266
                        UserId = customer.Id,
                        UserName = "customer@example.com",
                        Rating = 4,
                        Comment = "Module hoạt động ổn định, nhưng tài liệu hơi ít.",
                        ReviewDate = DateTime.Now.AddDays(-2)
                    }
                };
                context.ProductReviews.AddRange(reviews);
                context.SaveChanges();
            }
        }
    }
}
