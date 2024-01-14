﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using IMAPMonthlyFeeExtract.DataTier;
using IMAPMonthlyFeeExtract.Blob;
namespace IMAPMonthlyFeeExtract.BusinessTier
{
    public class FeeSolutionBO
    {
        public static async System.Threading.Tasks.Task CreateCSVAsync()
        {
            BlobContainer.AssignBlobObjects();
            DataTable dt= FeeSolutionDO.GetFeeSolutionData();
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns .Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            await BlobContainer.UploadBlocbAsync(sb.ToString());
        }
    }
}
