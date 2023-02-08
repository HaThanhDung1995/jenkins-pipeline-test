using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyPhamTrueLife.DAL.Base;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class dbDevNewContext : Microsoft.EntityFrameworkCore.DbContext, IContext, IDisposable
    {
        public dbDevNewContext()
        {
        }

        public dbDevNewContext(DbContextOptions<dbDevNewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InfoAddressDeliveryUser> InfoAddressDeliveryUsers { get; set; }
        public virtual DbSet<InfoCalendar> InfoCalendars { get; set; }
        public virtual DbSet<InfoCapacity> InfoCapacities { get; set; }
        public virtual DbSet<InfoCapacityProduct> InfoCapacityProducts { get; set; }
        public virtual DbSet<InfoCart> InfoCarts { get; set; }
        public virtual DbSet<InfoChangeImportOrder> InfoChangeImportOrders { get; set; }
        public virtual DbSet<InfoComent> InfoComents { get; set; }
        public virtual DbSet<InfoDetailCalendar> InfoDetailCalendars { get; set; }
        public virtual DbSet<InfoDetailImportSell> InfoDetailImportSells { get; set; }
        public virtual DbSet<InfoDistrict> InfoDistricts { get; set; }
        public virtual DbSet<InfoEvaluate> InfoEvaluates { get; set; }
        public virtual DbSet<InfoExchangeProduct> InfoExchangeProducts { get; set; }
        public virtual DbSet<InfoExpiryProduct> InfoExpiryProducts { get; set; }
        public virtual DbSet<InfoImageProduct> InfoImageProducts { get; set; }
        public virtual DbSet<InfoImportSell> InfoImportSells { get; set; }
        public virtual DbSet<InfoNature> InfoNatures { get; set; }
        public virtual DbSet<InfoNotificationsStaff> InfoNotificationsStaffs { get; set; }
        public virtual DbSet<InfoNotificationsUser> InfoNotificationsUsers { get; set; }
        public virtual DbSet<InfoOrder> InfoOrders { get; set; }
        public virtual DbSet<InfoOrderDetail> InfoOrderDetails { get; set; }
        public virtual DbSet<InfoPositionStaff> InfoPositionStaffs { get; set; }
        public virtual DbSet<InfoPriceProduct> InfoPriceProducts { get; set; }
        public virtual DbSet<InfoProduct> InfoProducts { get; set; }
        public virtual DbSet<InfoProductOutOfTime> InfoProductOutOfTimes { get; set; }
        public virtual DbSet<InfoProductPortfolio> InfoProductPortfolios { get; set; }
        public virtual DbSet<InfoPromotion> InfoPromotions { get; set; }
        public virtual DbSet<InfoPromotionBuyBonu> InfoPromotionBuyBonus { get; set; }
        public virtual DbSet<InfoPromotionDetail> InfoPromotionDetails { get; set; }
        public virtual DbSet<InfoPromotionProduct> InfoPromotionProducts { get; set; }
        public virtual DbSet<InfoPromotionType> InfoPromotionTypes { get; set; }
        public virtual DbSet<InfoProvince> InfoProvinces { get; set; }
        public virtual DbSet<InfoRefundProduct> InfoRefundProducts { get; set; }
        public virtual DbSet<InfoSever> InfoSevers { get; set; }
        public virtual DbSet<InfoStaff> InfoStaffs { get; set; }
        public virtual DbSet<InfoSupplier> InfoSuppliers { get; set; }
        public virtual DbSet<InfoTypeNature> InfoTypeNatures { get; set; }
        public virtual DbSet<InfoTypeProduct> InfoTypeProducts { get; set; }
        public virtual DbSet<InfoTypeStaff> InfoTypeStaffs { get; set; }
        public virtual DbSet<InfoTypeUser> InfoTypeUsers { get; set; }
        public virtual DbSet<InfoUser> InfoUsers { get; set; }

        public DbSet<T> Repository<T>() where T : class
        {
            return Set<T>();
        }

        public int SaveChange()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<InfoAddressDeliveryUser>(entity =>
            {
                entity.HasKey(e => e.AddressDeliveryId)
                    .HasName("PK__info_add__B9EF3DC788B09354");

                entity.ToTable("info_address_delivery_user");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.InfoAddressDeliveryUsers)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("fk_address_delivery_user_district");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.InfoAddressDeliveryUsers)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("fk_address_delivery_user_province");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoAddressDeliveryUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_address_delivery_user_user");
            });

            modelBuilder.Entity<InfoCalendar>(entity =>
            {
                entity.HasKey(e => e.CalendarId)
                    .HasName("PK__info_cal__53CFC44DD444D191");

                entity.ToTable("info_calendar");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoCapacity>(entity =>
            {
                entity.HasKey(e => e.CapacityId)
                    .HasName("PK__info_cap__5AEEAE1A139F6BB6");

                entity.ToTable("info_capacity");

                entity.Property(e => e.CapacityName).HasMaxLength(100);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoCapacityProduct>(entity =>
            {
                entity.HasKey(e => e.CapacityProductId)
                    .HasName("PK__info_cap__1634B13D18C1DBCF");

                entity.ToTable("info_capacity_product");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoCapacityProducts)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_capacity_product_capacity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoCapacityProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_capacity_product_product");
            });

            modelBuilder.Entity<InfoCart>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PK__info_car__51BCD7B7D2D5C516");

                entity.ToTable("info_cart");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.VoucherCode).HasMaxLength(500);

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoCarts)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_cart_capacity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoCarts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_cart_product");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.InfoCarts)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("fk_cart_promotion");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_cart_user");
            });

            modelBuilder.Entity<InfoChangeImportOrder>(entity =>
            {
                entity.HasKey(e => e.ChangeImportOrderId)
                    .HasName("PK__info_cha__6FCD044348F90D87");

                entity.ToTable("info_change_import_orders");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(500);

                entity.Property(e => e.StatusOfSupplier).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.ImportSell)
                    .WithMany(p => p.InfoChangeImportOrders)
                    .HasForeignKey(d => d.ImportSellId)
                    .HasConstraintName("fk_change_import_orders_import_sell");
            });

            modelBuilder.Entity<InfoComent>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.UserId, e.Times })
                    .HasName("PK__info_com__373B1C1923E8579E");

                entity.ToTable("info_coment");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoComents)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coment_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoComents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coment_user");
            });

            modelBuilder.Entity<InfoDetailCalendar>(entity =>
            {
                entity.HasKey(e => e.DetailCalendarId)
                    .HasName("PK__info_det__3E3148781185F87D");

                entity.ToTable("info_detail_calendar");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.InfoDetailCalendars)
                    .HasForeignKey(d => d.CalendarId)
                    .HasConstraintName("fk_detail_calendar_calendar");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.InfoDetailCalendars)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("fk_detail_calendar_staff");
            });

            modelBuilder.Entity<InfoDetailImportSell>(entity =>
            {
                entity.HasKey(e => new { e.ImportSellId, e.ProductId })
                    .HasName("PK__info_det__32DF3F409193BF07");

                entity.ToTable("info_detail_import_sell");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.Trademark).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoDetailImportSells)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_detail_import_sell_capacity");

                entity.HasOne(d => d.ImportSell)
                    .WithMany(p => p.InfoDetailImportSells)
                    .HasForeignKey(d => d.ImportSellId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_detail_import_sell_import_sell");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoDetailImportSells)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_detail_import_sell_product");
            });

            modelBuilder.Entity<InfoDistrict>(entity =>
            {
                entity.HasKey(e => e.DistrictId)
                    .HasName("PK__info_dis__85FDA4C65DD84B1F");

                entity.ToTable("info_district");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Prefix).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.InfoDistricts)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("fk_district_province");
            });

            modelBuilder.Entity<InfoEvaluate>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.UserId })
                    .HasName("PK__info_eva__65744A0933F12864");

                entity.ToTable("info_evaluate");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoEvaluates)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_evaluate_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoEvaluates)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_evaluate_user");
            });

            modelBuilder.Entity<InfoExchangeProduct>(entity =>
            {
                entity.HasKey(e => e.ExchangeProductId)
                    .HasName("PK__info_exc__1202ACC67D9A5514");

                entity.ToTable("info_exchange_product");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.InfoExchangeProducts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_exchange_product_order");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.InfoExchangeProducts)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("fk_exchange_product_staff");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoExchangeProducts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_exchange_product_user");
            });

            modelBuilder.Entity<InfoExpiryProduct>(entity =>
            {
                entity.HasKey(e => e.ExpiryProductId)
                    .HasName("PK__info_exp__FA343BFDB1F28256");

                entity.ToTable("info_expiry_product");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoExpiryProducts)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_expiry_product_capacity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoExpiryProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_expiry_product_product");
            });

            modelBuilder.Entity<InfoImageProduct>(entity =>
            {
                entity.HasKey(e => e.ImgProductId)
                    .HasName("PK__info_ima__A76ABE4DF40C50D9");

                entity.ToTable("info_image_product");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Img).HasColumnType("ntext");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoImageProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_image_product_product");
            });

            modelBuilder.Entity<InfoImportSell>(entity =>
            {
                entity.HasKey(e => e.ImportSellId)
                    .HasName("PK__info_imp__F99FF32CBD3DE011");

                entity.ToTable("info_import_sell");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.DateTimeD).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.InfoImportSells)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("fk_import_sell_staff");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.InfoImportSells)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("fk_import_sell_supplier");
            });

            modelBuilder.Entity<InfoNature>(entity =>
            {
                entity.HasKey(e => e.NatureId)
                    .HasName("PK__info_nat__B61719F1B70E692B");

                entity.ToTable("info_nature");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.NatureName).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.TypeNature)
                    .WithMany(p => p.InfoNatures)
                    .HasForeignKey(d => d.TypeNatureId)
                    .HasConstraintName("fk_nature_type_nature");
            });

            modelBuilder.Entity<InfoNotificationsStaff>(entity =>
            {
                entity.HasKey(e => e.NotificationsId)
                    .HasName("PK__info_not__94E5596DADA0E067");

                entity.ToTable("info_notifications_staff");

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.InfoNotificationsStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("fk_notifications_staff");
            });

            modelBuilder.Entity<InfoNotificationsUser>(entity =>
            {
                entity.HasKey(e => e.NotificationsId)
                    .HasName("PK__info_not__94E5596D14966443");

                entity.ToTable("info_notifications_user");

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoNotificationsUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_notifications_user");
            });

            modelBuilder.Entity<InfoOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__info_ord__C3905BCF5F1A3058");

                entity.ToTable("info_order");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.DateTimeD).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.StatusOrder).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.AddressDelivery)
                    .WithMany(p => p.InfoOrders)
                    .HasForeignKey(d => d.AddressDeliveryId)
                    .HasConstraintName("fk_order_address_delivery_user");

                entity.HasOne(d => d.Sever)
                    .WithMany(p => p.InfoOrders)
                    .HasForeignKey(d => d.SeverId)
                    .HasConstraintName("fk_order_sever");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.InfoOrders)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("fk_order_staff");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InfoOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_order_user");
            });

            modelBuilder.Entity<InfoOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__info_ord__08D097A33FE7339D");

                entity.ToTable("info_order_detail");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAd).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoOrderDetails)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_order_detail_capacity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.InfoOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_detail_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_detail_product");
            });

            modelBuilder.Entity<InfoPositionStaff>(entity =>
            {
                entity.HasKey(e => e.PositionStaffId)
                    .HasName("PK__info_pos__8A945224744A5321");

                entity.ToTable("info_position_staff");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.PositionStaffName).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoPriceProduct>(entity =>
            {
                entity.HasKey(e => e.PriceProductId)
                    .HasName("PK__info_pri__8B3F86C7110F6990");

                entity.ToTable("info_price_product");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoPriceProducts)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_price_product_capacity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoPriceProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_price_product_product");
            });

            modelBuilder.Entity<InfoProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__info_pro__B40CC6CDD1D14F0B");

                entity.ToTable("info_product");

                entity.Property(e => e.Avatar).HasColumnType("ntext");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasColumnType("ntext");

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.StatusProduct).HasMaxLength(500);

                entity.Property(e => e.Trademark).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Nature)
                    .WithMany(p => p.InfoProducts)
                    .HasForeignKey(d => d.NatureId)
                    .HasConstraintName("fk_product_nature");

                entity.HasOne(d => d.TypeProduct)
                    .WithMany(p => p.InfoProducts)
                    .HasForeignKey(d => d.TypeProductId)
                    .HasConstraintName("fk_product_type_product");
            });

            modelBuilder.Entity<InfoProductOutOfTime>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__info_pro__B40CC6CD3D2E2136");

                entity.ToTable("info_product_out_of_time");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.BrowsingStatus).HasMaxLength(500);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoProductOutOfTimes)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_product_out_of_time_capacity");

                entity.HasOne(d => d.ExpiryProduct)
                    .WithMany(p => p.InfoProductOutOfTimes)
                    .HasForeignKey(d => d.ExpiryProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_out_of_time_expiry_product");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.InfoProductOutOfTime)
                    .HasForeignKey<InfoProductOutOfTime>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_out_of_time_product");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.InfoProductOutOfTimes)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("fk_product_out_of_time_expiry_supplier");
            });

            modelBuilder.Entity<InfoProductPortfolio>(entity =>
            {
                entity.HasKey(e => e.ProductPortfolioId)
                    .HasName("PK__info_pro__9EC7DF6DEDA4DFC4");

                entity.ToTable("info_product_portfolio");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasMaxLength(500);

                entity.Property(e => e.ProductPortfolioName).HasMaxLength(200);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoPromotion>(entity =>
            {
                entity.HasKey(e => e.PromotionId)
                    .HasName("PK__info_pro__52C42FCF1A890826");

                entity.ToTable("info_promotion");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasMaxLength(500);

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.PromotionTypeNavigation)
                    .WithMany(p => p.InfoPromotions)
                    .HasForeignKey(d => d.PromotionType)
                    .HasConstraintName("fk_promotion_promotion_type");
            });

            modelBuilder.Entity<InfoPromotionBuyBonu>(entity =>
            {
                entity.HasKey(e => e.PromotionBonusId)
                    .HasName("PK__info_pro__63B7831548DE238D");

                entity.ToTable("info_promotion_buy_bonus");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.PromotionBuyBonus).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoPromotionBuyBonus)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_promotion_buy_bonus_product");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.InfoPromotionBuyBonus)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("fk_promotion_buy_bonus_promotion");
            });

            modelBuilder.Entity<InfoPromotionDetail>(entity =>
            {
                entity.HasKey(e => e.PromotionDetailId)
                    .HasName("PK__info_pro__FF43FD04EF76C0E2");

                entity.ToTable("info_promotion_detail");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InfoPromotionDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("fk_promotion_detail_product");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.InfoPromotionDetails)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("fk_promotion_detail_promotion");
            });

            modelBuilder.Entity<InfoPromotionProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__info_pro__B40CC6CD9006193C");

                entity.ToTable("info_promotion_product");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(500);

                entity.Property(e => e.StatusProduct).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoPromotionProducts)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_promotion_product_capacity");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.InfoPromotionProduct)
                    .HasForeignKey<InfoPromotionProduct>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_promotion_product_product");
            });

            modelBuilder.Entity<InfoPromotionType>(entity =>
            {
                entity.HasKey(e => e.PromotionType)
                    .HasName("PK__info_pro__0D1C52B0DCCDA734");

                entity.ToTable("info_promotion_type");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoProvince>(entity =>
            {
                entity.HasKey(e => e.ProvinceId)
                    .HasName("PK__info_pro__FD0A6F83F985915B");

                entity.ToTable("info_province");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoRefundProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__info_ref__B40CC6CDDC10F0F2");

                entity.ToTable("info_refund_product");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Capacity)
                    .WithMany(p => p.InfoRefundProducts)
                    .HasForeignKey(d => d.CapacityId)
                    .HasConstraintName("fk_refund_product_capacity");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.InfoRefundProduct)
                    .HasForeignKey<InfoRefundProduct>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_refund_product_product");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.InfoRefundProducts)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("fk_refund_product_supplier");
            });

            modelBuilder.Entity<InfoSever>(entity =>
            {
                entity.HasKey(e => e.SeverId)
                    .HasName("PK__info_sev__05DBC7B298ECEA3B");

                entity.ToTable("info_sever");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SeverName).HasMaxLength(200);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoStaff>(entity =>
            {
                entity.HasKey(e => e.StaffId)
                    .HasName("PK__info_sta__96D4AB1714D9EFC3");

                entity.ToTable("info_staff");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Avatar).HasColumnType("ntext");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.InfoStaffs)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("fk_staff_district");

                entity.HasOne(d => d.PositionStaff)
                    .WithMany(p => p.InfoStaffs)
                    .HasForeignKey(d => d.PositionStaffId)
                    .HasConstraintName("fk_staff_position_staff");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.InfoStaffs)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("fk_staff_province");

                entity.HasOne(d => d.TypeStaff)
                    .WithMany(p => p.InfoStaffs)
                    .HasForeignKey(d => d.TypeStaffId)
                    .HasConstraintName("fk_staff_type_staff");
            });

            modelBuilder.Entity<InfoSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK__info_sup__4BE666B420A96787");

                entity.ToTable("info_supplier");

                entity.Property(e => e.Adrress).HasMaxLength(100);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierName).HasMaxLength(200);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.InfoSuppliers)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("fk_supplier_district");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.InfoSuppliers)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("fk_supplier_province");

                entity.HasOne(d => d.TypeProduct)
                    .WithMany(p => p.InfoSuppliers)
                    .HasForeignKey(d => d.TypeProductId)
                    .HasConstraintName("fk_supplier_type_product");
            });

            modelBuilder.Entity<InfoTypeNature>(entity =>
            {
                entity.HasKey(e => e.TypeNatureId)
                    .HasName("PK__info_typ__A1178181735B96D4");

                entity.ToTable("info_type_nature");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.TypeNatureName).HasMaxLength(100);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoTypeProduct>(entity =>
            {
                entity.HasKey(e => e.TypeProductId)
                    .HasName("PK__info_typ__90707413F2B71814");

                entity.ToTable("info_type_product");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.TypePeoductName).HasMaxLength(200);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.ProductPortfolio)
                    .WithMany(p => p.InfoTypeProducts)
                    .HasForeignKey(d => d.ProductPortfolioId)
                    .HasConstraintName("fk_type_product_product_portfolio");
            });

            modelBuilder.Entity<InfoTypeStaff>(entity =>
            {
                entity.HasKey(e => e.TypeStaffId)
                    .HasName("PK__info_typ__49BCCE8DA72A8B51");

                entity.ToTable("info_type_staff");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.TypeStaffName).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoTypeUser>(entity =>
            {
                entity.HasKey(e => e.TypeUserId)
                    .HasName("PK__info_typ__49E1F53DA241C197");

                entity.ToTable("info_type_user");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.TypeUserName).HasMaxLength(500);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<InfoUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__info_use__1788CC4C0D8998B9");

                entity.ToTable("info_user");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Avatar).HasColumnType("ntext");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.InfoUsers)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("fk_user_district");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.InfoUsers)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("fk_user_province");

                entity.HasOne(d => d.TypeUser)
                    .WithMany(p => p.InfoUsers)
                    .HasForeignKey(d => d.TypeUserId)
                    .HasConstraintName("fk_user_type_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
