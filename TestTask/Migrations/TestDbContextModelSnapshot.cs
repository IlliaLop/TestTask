// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTask.DataModel;

#nullable disable

namespace TestTask.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestTask.Account", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.HasIndex("ContactId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("TestTask.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("TestTask.Incident", b =>
                {
                    b.Property<string>("IncidentName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncidentName");

                    b.HasIndex("AccountName");

                    b.ToTable("Incident");
                });

            modelBuilder.Entity("TestTask.Account", b =>
                {
                    b.HasOne("TestTask.Contact", "Contact")
                        .WithMany("Account")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("TestTask.Incident", b =>
                {
                    b.HasOne("TestTask.Account", "Account")
                        .WithMany("Incident")
                        .HasForeignKey("AccountName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("TestTask.Account", b =>
                {
                    b.Navigation("Incident");
                });

            modelBuilder.Entity("TestTask.Contact", b =>
                {
                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
