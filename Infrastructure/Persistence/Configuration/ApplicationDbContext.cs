using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configuration;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Consumed> Consumeds { get; set; }

    public virtual DbSet<Difference> Differences { get; set; }

    public virtual DbSet<Entry> Entries { get; set; }

    public virtual DbSet<Existence> Existences { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Lost> Losts { get; set; }

    public virtual DbSet<Money> Money { get; set; }

    public virtual DbSet<Move> Moves { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<ReturnsDetail> ReturnsDetails { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesDetail> SalesDetails { get; set; }

    public virtual DbSet<Turn> Turns { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersPermission> UsersPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnType("TEXT(256)");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnType("TEXT(512)");
            entity.Property(e => e.Email).HasColumnType("TEXT(64)");
            entity.Property(e => e.Identity)
                .HasColumnType("TEXT(16)")
                .HasColumnName("identity");
            entity.Property(e => e.Name).HasColumnType("TEXT(512)");
            entity.Property(e => e.Phone).HasColumnType("TEXT(32)");
        });

        modelBuilder.Entity<Consumed>(entity =>
        {
            entity.ToTable("Consumed");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdEntry).HasColumnName("id_entry");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.IdEntryNavigation).WithMany(p => p.Consumeds).HasForeignKey(d => d.IdEntry);
        });

        modelBuilder.Entity<Difference>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DiffDate)
                .HasColumnType("text(128)")
                .HasColumnName("diff_date");
            entity.Property(e => e.Existence).HasColumnName("existence");
            entity.Property(e => e.Fixed)
                .HasColumnType("integer(1)")
                .HasColumnName("fixed");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdTurn).HasColumnName("id_turn");
            entity.Property(e => e.Real).HasColumnName("real");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Differences).HasForeignKey(d => d.IdProduct);

            entity.HasOne(d => d.IdTurnNavigation).WithMany(p => p.Differences).HasForeignKey(d => d.IdTurn);
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdMove).HasColumnName("id_move");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.IdMoveNavigation).WithMany(p => p.Entries)
                .HasForeignKey(d => d.IdMove)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Existence>(entity =>
        {
            entity.ToTable("Existence");

            entity.HasIndex(e => new { e.PurchasePrice, e.SalePrice, e.IdProduct }, "IX_Existence_purchase_price_sale_price_id_product").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.PurchasePrice)
                .HasDefaultValue(0.0)
                .HasColumnName("purchase_price");
            entity.Property(e => e.SalePrice)
                .HasDefaultValue(0.0)
                .HasColumnName("sale_price");
            entity.Property(e => e.SaleStock)
                .HasDefaultValue(0.0)
                .HasColumnName("sale_stock");
            entity.Property(e => e.StoreStock)
                .HasDefaultValue(0.0)
                .HasColumnName("store_stock");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Existences).HasForeignKey(d => d.IdProduct);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasIndex(e => new { e.IdProductMain, e.IdProductIngredient }, "IX_Ingredients_id_product_main_id_product_ingredient").IsUnique();

            entity.HasIndex(e => new { e.IdProductMain, e.IdProductIngredient }, "idx_product").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdProductIngredient).HasColumnName("id_product_ingredient");
            entity.Property(e => e.IdProductMain).HasColumnName("id_product_main");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdProductIngredientNavigation).WithMany(p => p.IngredientIdProductIngredientNavigations)
                .HasForeignKey(d => d.IdProductIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdProductMainNavigation).WithMany(p => p.IngredientIdProductMainNavigations).HasForeignKey(d => d.IdProductMain);
        });

        modelBuilder.Entity<Lost>(entity =>
        {
            entity.ToTable("Lost");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Lost)
                .HasForeignKey<Lost>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Money>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("real(16,2)")
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasColumnType("TEXT(1024)")
                .HasColumnName("description");
            entity.Property(e => e.IdMove).HasColumnName("id_move");
            entity.Property(e => e.IdTurn).HasColumnName("id_turn");

            entity.HasOne(d => d.IdMoveNavigation).WithMany(p => p.Money).HasForeignKey(d => d.IdMove);

            entity.HasOne(d => d.IdTurnNavigation).WithMany(p => p.Money)
                .HasForeignKey(d => d.IdTurn)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Move>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Concept)
                .HasColumnType("text(3)")
                .HasColumnName("concept");
            entity.Property(e => e.Date)
                .HasColumnType("text(24)")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasColumnType("text(5000)")
                .HasColumnName("description");
            entity.Property(e => e.IdTurn).HasColumnName("id_turn");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdTurnNavigation).WithMany(p => p.Moves).HasForeignKey(d => d.IdTurn);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Moves)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("Owner");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text(512)")
                .HasColumnName("address");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Message)
                .HasColumnType("text(512)")
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasColumnType("text(256)")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("text(32)")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Permissions_name").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Area)
                .HasColumnType("TEXT(128)")
                .HasColumnName("area");
            entity.Property(e => e.Description)
                .HasColumnType("text(1024)")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("text(32)")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.Id, "id_idx").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Composed)
                .HasColumnType("integer(1)")
                .HasConversion<int>()
                .HasColumnName("composed");
            entity.Property(e => e.Description)
                .HasColumnType("text(1024)")
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name)
                .HasColumnType("text(512)")
                .HasColumnName("name");
            entity.Property(e => e.Um)
                .HasColumnType("TEXT(6)")
                .HasColumnName("um");
            entity.Property(e => e.Unitary)
                .HasColumnType("integer(1)")
                .HasConversion<int>()
                .HasColumnName("unitary");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdSale)
                .HasColumnType("integer(16)")
                .HasColumnName("id_sale");
            entity.Property(e => e.Paid).HasColumnName("paid");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Return)
                .HasForeignKey<Return>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.Returns).HasForeignKey(d => d.IdSale);
        });

        modelBuilder.Entity<ReturnsDetail>(entity =>
        {
            entity.ToTable("Returns_Details");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdReturn).HasColumnName("id_return");
            entity.Property(e => e.IdSaleDetail).HasColumnName("id_sale_detail");
            entity.Property(e => e.Toexpenses)
                .HasColumnType("integer(1)")
                .HasColumnName("toexpenses");

            entity.HasOne(d => d.IdReturnNavigation).WithMany(p => p.ReturnsDetails)
                .HasForeignKey(d => d.IdReturn)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSaleDetailNavigation).WithMany(p => p.ReturnsDetails)
                .HasForeignKey(d => d.IdSaleDetail)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DatePaid)
                .HasColumnType("text(24)")
                .HasColumnName("date_paid");
            entity.Property(e => e.DatePaidLimit)
                .HasColumnType("text(24)")
                .HasColumnName("date_paid_limit");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Invoice)
                .HasColumnType("text(16)")
                .HasColumnName("invoice");
            entity.Property(e => e.Paid)
                .HasColumnType("integer(1)")
                .HasColumnName("paid");
            entity.Property(e => e.PaidMethod)
                .HasColumnType("TEXT(64)")
                .HasColumnName("paid_method");
            entity.Property(e => e.Reference)
                .HasColumnType("TEXT(512)")
                .HasColumnName("reference");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Sale).HasForeignKey<Sale>(d => d.Id);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Sales).HasForeignKey(d => d.IdClient);
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.ToTable("Sales_Details");

            entity.HasIndex(e => new { e.Id, e.IdSale }, "idx_id_sale").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FinalPrice).HasColumnName("final_price");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdSale).HasColumnName("id_sale");
            entity.Property(e => e.PurchasePrice).HasColumnName("purchase_price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Turn>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Turns)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username, "IX_Users_username").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Disabled)
                .HasColumnType("integer(1)")
                .HasConversion<int>()
                .HasColumnName("disabled");
            entity.Property(e => e.Email)
                .HasColumnType("TEXT(64)")
                .HasColumnName("email");
            entity.Property(e => e.Isadmin)
                .HasDefaultValue(0)
                .HasConversion<int>()
                .HasColumnType("integer(1)")
                .HasColumnName("isadmin");
            entity.Property(e => e.Name)
                .HasColumnType("TEXT(128)")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("TEXT(1024)")
                .HasColumnName("password");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Position)
                .HasDefaultValue("")
                .HasColumnType("TEXT(512)")
                .HasColumnName("position");
            entity.Property(e => e.Salary)
                .HasColumnType("text(256)")
                .HasColumnName("salary");
            entity.Property(e => e.SharedPassword)
                .HasColumnType("TEXT(1024)")
                .HasColumnName("shared_password");
            entity.Property(e => e.Username)
                .HasColumnType("TEXT(64)")
                .HasColumnName("username");
        });

        modelBuilder.Entity<UsersPermission>(entity =>
        {
            entity.ToTable("Users_Permissions");

            entity.HasIndex(e => new { e.IdUser, e.IdPermission }, "IX_Users_Permissions_id_user_id_permission").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Granted)
                .HasColumnType("integer(1)")
                .HasConversion<int>()
                .HasColumnName("granted");
            entity.Property(e => e.IdPermission).HasColumnName("id_permission");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.RequestElevation)
                .HasColumnType("integer(1)")
                .HasConversion<int>()
                .HasColumnName("request_elevation");

            entity.HasOne(d => d.IdPermissionNavigation).WithMany(p => p.UsersPermissions).HasForeignKey(d => d.IdPermission);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UsersPermissions).HasForeignKey(d => d.IdUser);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
