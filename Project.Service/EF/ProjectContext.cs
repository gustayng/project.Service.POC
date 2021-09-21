using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Project.Service
{
    public partial class ProjectContext : DbContext
    {
        public string AccessToken { get; set; }

        public string ConnectionString { get; set; }
        public ProjectContext(string connectionString)
        {
                this.ConnectionString = connectionString;
            
        }

        public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
        { }

        public ProjectContext(DbContextOptions<ProjectContext> options, IConfiguration conf)
            : base(options)
        {
            //var MSIClientID = conf.GetSection("AppSettings").GetSection("MSIClientID").Value;
            //var connection = (SqlConnection)Database.GetDbConnection();
            //var connecitonOptions = "RunAs=App;AppId=" + MSIClientID;
            //connection.AccessToken = (new Microsoft.Azure.Services.AppAuthentication.AzureServiceTokenProvider(connecitonOptions)).GetAccessTokenAsync("https://database.windows.net/").Result;
            //this.AccessToken = connection.AccessToken;
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Project.Service;persist security info=True;Integrated Security=SSPI;");
                optionsBuilder.UseSqlServer(this.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(p => new { p.Assignment_Id });
                entity.ToTable("Assignment");

                //entity.Property(e => e.Assignment_Id)
                //    .ValueGeneratedNever()
                //    .HasColumnName("Assignment_Id");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                //entity.HasOne(d => d.AssignmentGroup)
                //.WithMany(p => p.Assignments)
                //.HasForeignKey(p => p.AssignmentGroup_Id)
                //.HasConstraintName("FK_Assignment_AssignmentGroup");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => new { p.Project_Id });

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Project_Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ProjectType)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.ProjectType_Id)
                .HasConstraintName("FK_Project_ProjectType");

            });

            modelBuilder.Entity<ProjectType>(entity =>
            {
                entity.HasKey(p => new { p.ProjectType_Id });

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProjectType_Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

            });

            OnModelCreatingPartial(modelBuilder);


        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
