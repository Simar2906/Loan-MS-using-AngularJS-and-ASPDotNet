using Npgsql;
using Dapper;

namespace Image_Downloader
{
    internal class Program
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=LMS2;Username=postgres;Password=simarjit;";
        private const string ImageFolder = @"..\..\Loan Management System\src\app\assets\";

        static async Task Main(string[] args)
        {

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                var loanImages = await connection.QueryAsync<LoanImage>("SELECT \"Id\", \"Logo\" FROM \"Loans\" WHERE \"logofileid\" IS NULL");
                foreach (var loanImage in loanImages)
                {
                    var fileId = await DownloadAndSaveImage(loanImage.Logo);
                    await connection.ExecuteAsync("UPDATE \"Loans\" SET logofileid = @FileId WHERE \"Id\" = @Id", new { FileId = fileId, loanImage.Id });
                }

                var userImages = await connection.QueryAsync<UserImage>("SELECT \"Id\", \"UserPic\" FROM \"Users\" WHERE \"profilepicturefileid\" IS NULL");
                foreach (var userImage in userImages)
                {
                    var fileId = await DownloadAndSaveImage(userImage.UserPic);
                    await connection.ExecuteAsync("UPDATE \"Users\" SET \"profilepicturefileid\" = @FileId WHERE \"Id\" = @Id", new { FileId = fileId, userImage.Id });
                }
            }

        }
        private static async Task<int> DownloadAndSaveImage(string url)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("..", "..", "..", "..", "Loan Management System", "src", "assets");
            string assetsFolderPath = Path.GetFullPath(Path.Combine(basePath, relativePath));
            using (var httpClient = new HttpClient())
            {
                var imageBytes = await httpClient.GetByteArrayAsync(url);
                var imageName = Path.GetFileName(url);
                var fileName = Path.Combine(assetsFolderPath, Path.GetFileName(url));
                await File.WriteAllBytesAsync(fileName, imageBytes);
                var localPath = Path.Combine("src", "assets", imageName);
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var fileId = await connection.ExecuteScalarAsync<int>(
                        "INSERT INTO Files (FilePath) VALUES (@FilePath) RETURNING Id",
                        new { FilePath = localPath });
                    return fileId;
                }
            }
        }
    }
    public class LoanImage
    {
        public int Id { get; set; }
        public string Logo { get; set; }
    }

    public class UserImage
    {
        public int Id { get; set; }
        public string UserPic { get; set; }
    }
}
