using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace WebAPI_PTS.Models
{
    public class TrialsContext : DbContext
    {
        protected TrialsContext(DbContextOptions<TrialsContext> options) : base(options)
        {
        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }*/

        public DbSet<FieldBlock> FieldBlocks { get; set; }
        public DbSet<SubBlock> SubBlocks { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<TrialType> TrialType { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropCategory> CropCategories { get; set;  }
        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }
        public DbSet<ResultEntry> ResultVariableType { get; set; }
        public DbSet<ResultType> ResultTypes { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
    }
    public class FieldBlock {
        public int FieldBlockId { get; set; }
        public string BlockChar { get; set; }
        public string Description { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class SubBlock
    {
        public int SubBlockId { get; set; }       
        public int TrialType { get; set; }
        public string TrialDescription { get; set; }
        public Comment SubBlockComment { get; set; }

        public int FieldBlockId { get; set; }
        public FieldBlock FieldBlock { get; set; }
    }


    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductOwner { get; set; }
        public int ProductCategoryId { get; set; }
    }

    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }

    public class TrialType
    {
        public int TrialTypeId { get; set; }
        public string TrialTypeName { get; set; }
    }

    public class TrialGroup
    {
        public int FieldBlockId { get; set; }
    }

    public class Crop
    {
        public int CropId { get; set; }
        public int CropName { get; set; }
        public int CropCategoryId { get; set; }
    }


    public class CropCategory
    {
        public int CropCategoryId { get; set; }
        public int CropCategoryName { get; set; }
    }

    public class Treatment
    {
        public int TreatmentId { get; set; }
        public int TrialGroupId { get; set; }
        public int TreatmentTypeId { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string TreatmentStage { get; set; }
        public List<TreamentProduct> ProductDosages { get; set; }
    }
    public class TreamentProduct
    {
        public int TreatmentProductId { get; set; }
        public int TreatmentId { get; set; }
        public int ProductId { get; set; }
        
    }

    public class TreatmentType
    {
        public int TreatmentTypeId { get; set; }
        public string TreatmentTypeName { get; set; }
    }

    public class ResultEntry
    {
        public int ResultEntryId { get; set; }
        public double Value { get; set; }

        public int ResultGroupId { get; set; }
        public ResultGroup ResultGroup { get; set; }
    }
    public class ResultType
    {
        public int ResultTypeId { get; set; }
    }

    public class ResultTypeTreatmentProduct
    {
        public int ResultTypeId { get; set; }
    }
    public class UnitType
    {
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
    }
    public class TreatmentComment
    {
        public int TreatmentCommentId { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
    }

    public class ResultGroup
    {
        public int TrialGroupId { get; set; }
        public int TreatmentId { get; set; }
        public int ResultFormatId { get; set; }
    }

    public class TrialGroupResults
    {
        public TrialGroup CommentId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
    }


    public class TreatmentImage
    {
        public int ImageId { get; set; }
        public byte[] ImageArr { get; set; }
        public string Description { get; set; }

        public int TreatmentId { get; set; }
    }


    public class DosageType
    {
        public int DosageTypeId { get; set; }
        public string DosageName { get; set; }

        public List<DosageAmount> Amounts { get; set; }
    }

    public class DosageAmount
    {
        public double DosageAmountId{ get; set; }
        public double Value { get; set; }

        public int DosageTypeId { get; set; }
    }

    public class ResultFormat
    {
        public int ResultEntryId { get; set; }
        public double Value { get; set; }

        public int ResultGroupId { get; set; }
        public ResultGroup ResultGroup { get; set; }
    }
}
