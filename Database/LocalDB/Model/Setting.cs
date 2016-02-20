using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IMS.Database.LocalDB.Model
{
    public class Setting
    {
        public int Idx { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int Code { get; set; }
        public string Type { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Setting()
        {
            
        }

        public void Set(string key, object value)
        {
            Key = key;
            if (value is int)
            {
                Value = ((int) value).ToString();
                Code = 0;
                Type = "Integer";
                UpdatedAt = DateTime.Now;
            }
            else if (value is float)
            {
                Value = ((float)value).ToString();
                Code = 1;
                Type = "Float";
                UpdatedAt = DateTime.Now;
            }
            else if (value is double)
            {
                Value = ((double)value).ToString();
                Code = 2;
                Type = "Double";
                UpdatedAt = DateTime.Now;
            }
            else if (value is string)
            {
                Value = (string) value;
                Code = 3;
                Type = "String";
                UpdatedAt = DateTime.Now;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public int GetInt()
        {
            int result;
            if (int.TryParse(Value, out result))
                return result;
            return int.MinValue;
        }

        public float GetFloat()
        {
            float result;
            if (float.TryParse(Value, out result))
                return result;
            return float.MinValue;
        }

        public double GetDouble()
        {
            double result;
            if (double.TryParse(Value, out result))
                return result;
            return double.MinValue;
        }

        public string GetString()
        {
            return Value;
        }
    }

    public class SettingConfiguration : EntityTypeConfiguration<Setting>
    {
        public SettingConfiguration()
        {
            ToTable("Setting");
            HasKey(s => s.Idx);
            Property(s => s.Idx)
                .IsRequired()
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(s => s.Key)
                .IsRequired()
                .HasMaxLength(2048)
                .IsUnicode(true)
                .HasColumnAnnotation("Idx_Key", new IndexAnnotation(new IndexAttribute()))
                .HasColumnOrder(1);
            Property(s => s.Value)
                .IsOptional()
                .IsUnicode(true)
                .HasColumnType("text")
                .HasColumnOrder(2);
            Property(s => s.Code)
                .IsRequired()
                .HasColumnOrder(3);
            Property(s => s.Type)
                .IsOptional()
                .HasMaxLength(32)
                .IsUnicode(true)
                .HasColumnOrder(4);
            Property(s => s.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasColumnOrder(5);
        }
    }
}