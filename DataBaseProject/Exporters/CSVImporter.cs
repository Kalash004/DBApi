using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Sylvan.Data.Csv;

namespace DataBaseProject.Exporters
{
    internal class CSVImporter
    {
        public void ImportHaircuts(String path)
        {
            string table = "Haircuts";
            SqlConnection conn = new DataBaseConnection().GetInstance();
            var cmd = conn.CreateCommand();
            cmd.CommandText = String.Format("select top 0 * from {0}", table);
            var reader = cmd.ExecuteReader();
            var tableSchema = reader.GetColumnSchema();
            reader.Close();
            var options =
                new CsvDataReaderOptions
                {
                    Schema = new CsvSchema(tableSchema)
                };

            using var csv = CsvDataReader.Create(path, options);
            var bcp = new SqlBulkCopy(conn);
            bcp.BulkCopyTimeout = 0;
            bcp.DestinationTableName = table;
            bcp.WriteToServer(csv);
        }

        public void ImportPaint(String path)
        {
            string table = "Paints";
            SqlConnection conn = new DataBaseConnection().GetInstance();
            var cmd = conn.CreateCommand();
            cmd.CommandText = String.Format("select top 0 * from {0}", table);
            var reader = cmd.ExecuteReader();
            var tableSchema = reader.GetColumnSchema();
            reader.Close();
            var options =
                new CsvDataReaderOptions
                {
                    Schema = new CsvSchema(tableSchema)
                };

            using var csv = CsvDataReader.Create(path, options);
            var bcp = new SqlBulkCopy(conn);
            bcp.BulkCopyTimeout = 0;
            bcp.DestinationTableName = table;
            bcp.WriteToServer(csv);
        }

        public void ImportUser(String path)
        {
            string table = "Users";
            SqlConnection conn = new DataBaseConnection().GetInstance();
            var cmd = conn.CreateCommand();
            cmd.CommandText = String.Format("select top 0 * from {0}", table);
            var reader = cmd.ExecuteReader();
            var tableSchema = reader.GetColumnSchema();
            reader.Close();
            var options =
                new CsvDataReaderOptions
                {
                    Schema = new CsvSchema(tableSchema)
                };

            using var csv = CsvDataReader.Create(path, options);
            var bcp = new SqlBulkCopy(conn);
            bcp.BulkCopyTimeout = 0;
            bcp.DestinationTableName = table;
            bcp.WriteToServer(csv);
        }

        //    private void InsertCSVRecords(DataTable csvdt, T obj, string tablename)
        //    {
        //        SqlConnection conn = new DataBaseConnection().GetInstance();
        //        SqlBulkCopy objbulk = new SqlBulkCopy(conn);
        //        //assigning Destination table name    
        //        objbulk.DestinationTableName = tablename;
        //        //Mapping Table column    
        //        foreach (var itm in Map(obj))
        //        {
        //            objbulk.ColumnMappings.Add(itm);
        //        }
        //        //          objbulk.ColumnMappings.Add("Designation", "Designation");
        //        //inserting Datatable Records to DataBase

        //        try
        //        {
        //            conn.Open();
        //            objbulk.WriteToServer(csvdt);
        //            conn.Close();
        //        } catch (Exception e)
        //        {
        //            throw;
        //        }
        //    }

        //    public abstract IEnumerable<SqlBulkCopyColumnMapping> Map(T obj);
    }
}
