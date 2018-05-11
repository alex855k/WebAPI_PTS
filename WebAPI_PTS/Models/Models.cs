using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_PTS.Models
{
    public class TrialsContext : DbContext
    {
        protected TrialsContext(DbContextOptions<TrialsContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FieldBlock>()
                .Property(p => p.Active)
                .HasDefaultValue(true);
        }

        public DbSet<FieldBlock> FieldBlocks { get; set; }
        public DbSet<TrialBlock> TrialBlocks { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<TrialType> TrialType { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropType> CropCategories { get; set;  }
        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }
        public DbSet<ResultEntry> ResultVariableType { get; set; }
        public DbSet<RequiredResultFormat> ResultTypes { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
    }
    public class FieldBlock {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FieldBlockId { get; set; }
        [Required]
        [StringLength(20)]
        public string BlockChar { get; set; }
        [Required(ErrorMessage = "Creation year is required, for documentation purposes."), Range(2000, 2050, ErrorMessage = "Please enter a valid year")]
        public int YearCreated { get; set; }
        [Column(TypeName = "TEXT")]
        public string Description { get; set; }
        public int FieldBlockLength { get; set; }
        public int FieldBlockWidth { get; set; }
        public bool Active { get; set; }

        public List<TrialBlock> TrialBlocks {get;set;}
    }

    public class TrialBlock
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialBlockId { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Year for the start of the trial is required.")]
        public int TrialStart { get; set; }
        [Required(ErrorMessage = "Year for the end of the trial is required.")]
        public int TrialEnd { get; set; }

        [ForeignKey("TrialTypeId")]
        public int TrialTypeId { get; set; }
        public TrialType TrialType { get; set; }
        public int FieldBlockId { get; set; }
        public FieldBlock FieldBlock { get; set; }
        public List<RequiredResultFormat> RequiredResultsFormat { get; set; }
    }


    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [MaxLength(100)]
        public string ProductOwner { get; set; }
        [Required]
        [ForeignKey("ProductCategoryId")]
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }

    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductCategoryId { get; set; }
        [Required, MaxLength(50,ErrorMessage = "Category name is too long."), DataType("NVARCHAR")]
        public string ProductCategoryName { get; set; }

        public List<Product> Products { get; set; }
    }

    public class TrialType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialTypeId { get; set; }
        [Required, MaxLength(30)]
        public string TrialTypeName { get; set; }
        [DataType("NVARCHAR"), MaxLength(4000)]
        public string Description { get; set; }
    }

    public class TrialGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialGroupId { get; set; }
        public int CropId { get; set; }
        public Crop Crop { get; set; }

        public int FieldBlockId { get; set; }
        public FieldBlock FieldBlock { get; set; }
        public List<TrialObservation> TrialObservations { get; set; }

    }

    public class TrialObservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrialObservationId { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int TrialGroupId{ get; set; }
        public TrialGroup TrialGroup { get; set; }
    }

    public class Crop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CropId { get; set; }
        [Required, DataType("NVARCHAR"), MaxLength(60, ErrorMessage = "Name is too long.")]
        public int CropName { get; set; }
        [NotMapped]
        public string CropTypeName { get { switch (CropType) { case null: return ""; default: return CropType.CropTypeName; } } }


        public CropType CropType { get; set; }
        public List<TrialGroup> TrialGroups { get; set; }
    }


    public class CropType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CropTypeId { get; set; }
        [Required, DataType("NVARCHAR"),MaxLength(60, ErrorMessage ="Name is too long.")]
        public string CropTypeName { get; set; }
    }

    public class Treatment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TreatmentId { get; set; }
        public int TreatmentTypeId { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string TreatmentStage { get; set; }
        
        public int TrialGroupId { get; set; }
        public TrialGroup TrialGorup { get; set; }
        public List<TreamentProduct> ProductDosages { get; set; }
    }
    public class TreamentProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TreatmentProductId { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class TreatmentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TreatmentTypeId { get; set; }

        public string TreatmentTypeName { get; set; }
    }

    public class ResultEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultEntryId { get; set; }
        public double ResultValue { get; set; }

        public int RequiredResultFormatId { get; set; }
        public RequiredResultFormat RequiredResultFormat { get; set; }
        public int DosageAmountId { get; set; }
        public DosageAmount DosageAmount { get; set; }
        public int TrialGroupId { get; set; }
        public TrialGroup TrialGroup { get; set; }
     
    }

  
    public class UnitType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
    }
    public class TreatmentComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TreatmentCommentId { get; set; }
        [Required, ForeignKey("Treatment")]
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }

    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
    }


    public class TreatmentImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        [Required]
        public byte[] ImageByteArr { get; set; }
        [MaxLength(250), DataType("NVARCHAR")]
        public string Caption { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }
    }


    public class DosageType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DosageTypeId { get; set; }
        [Required, StringLength(30, ErrorMessage="Use a shorter name."), DataType("NVARCHAR")]
        public string DosageName { get; set; }

        [ForeignKey("UnitType"), Required]
        public int UnitTypeId { get; set; }
        public UnitType UnitType { get; set; }
        public List<DosageAmount> Amounts { get; set; }
    }

    public class DosageAmount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public double DosageAmountId{ get; set; }
        [Required, MinLength(0, ErrorMessage = "Can't enter a negative value.")]
        public double Value { get; set; }

        public int DosageTypeId { get; set; }
        public DosageType DosageType { get; set; }
    }

    public class RequiredResultFormat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultEntryId { get; set; }
        [Required(ErrorMessage ="Enter a title for the result format."), DataType("NVARCHAR"), MaxLength(60, ErrorMessage = "Title is too long."), MinLength(60, ErrorMessage = "Title is too short.")]
        public string Title  { get; set; }
        [Required(ErrorMessage = "Enter a title for the result format."), DataType("NVARCHAR"), MaxLength(60, ErrorMessage = "Title is too long."), MinLength(60, ErrorMessage = "Title is too short.")]
        public double Description { get; set; }

        public int TrialBlockId { get; set; }
        public TrialBlock TrialBlock { get; set; }
        [ForeignKey("UnitType"), Required]
        public string UnitType { get; set; }
        public UnitType UnitTypeId { get; set; }
        public List<ResultEntry> ResultEntries { get; set; }
    }
}
