﻿using CsvJoin.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CsvJoin
{
    public class Application
    {
        private readonly ISqlPreparator _preparator;
        private readonly ISqlFormatter _formatter;
        private readonly ISqlExecutor _executor;
        private readonly ISqlSaver _saver;

        public Application(
            ISqlPreparator preparator,
            ISqlFormatter formatter,
            ISqlExecutor executor,
            ISqlSaver saver)
        {
            _preparator = preparator;
            _formatter = formatter;
            _executor = executor;
            _saver = saver;
        }

        public async Task RunAsync(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException(nameof(args));
            }

            string directory = args.First();
            string[] fileNames = args.Skip(1).Take(2).ToArray();

            string sql = _preparator.PrepareFullJoinSql(directory, fileNames);

            sql = _formatter.FormatSql(sql, indentChar: ' ');

            string connectionString = @$"Provider=Microsoft.ACE.OLEDB.16.0;
                Data Source={directory};
                OLE DB Services=-1;
                Extended Properties=""text;Excel 16.0;HDR=YES;IMEX=1""";

            var output = Console.OpenStandardOutput();

            await _executor.ExecuteSqlAsync(
                sql,
                connectionString,
                output);

            await _saver.SaveSqlAsync(sql, filePath: "SQLQuery.sql");
        }
    }
}
