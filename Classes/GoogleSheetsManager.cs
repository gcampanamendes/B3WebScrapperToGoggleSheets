using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace B3WebScrapperToGoggleSheets.Classes
{
    public class GoogleSheetsManager
    {
        private GoogleCredential credential;
        private SheetsService service;
        private string[] Scopes = { SheetsService.Scope.Spreadsheets, SheetsService.Scope.DriveFile };

        public GoogleCloudServiceAccountInfo CredentialsInfo = new GoogleCloudServiceAccountInfo();
        public bool Authenticated = false;

        public GoogleSheetsManager()
        {
            ;
        }

        public bool Authenticate(string credentialsFilePath)
        {
            CredentialsInfo.Update(credentialsFilePath);

            using (var stream = new FileStream(credentialsFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });

            return Authenticated = (service != null && credential != null);
        }

        public IList<IList<Object>> ReadSheet(string spreadsheetId, string range)
        {
            if (string.IsNullOrWhiteSpace(spreadsheetId) || (string.IsNullOrEmpty(range)))
                throw new ArgumentNullException(nameof(spreadsheetId));

            if (!Authenticated)
                return new List<IList<object>>();

            if (SheetExists(spreadsheetId, range))
            {
                var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
                var response = request.Execute();

                return response.Values;
            }

            return new List<IList<object>>();
        }

        public void WriteSheet(string spreadsheetId, string range, IList<IList<Object>> values)
        {
            var valueRange = new ValueRange
            {
                Values = values
            };

            var updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            updateRequest.Execute();
        }

        public void ClearSheet(string spreadsheetId, string range)
        {
            var clearRequest = new ClearValuesRequest();
            var request = service.Spreadsheets.Values.Clear(clearRequest, spreadsheetId, range);
            request.Execute();
        }

        public bool SheetExists(string spreadsheetId, string sheetName)
        {
            var spreadsheet = service.Spreadsheets.Get(spreadsheetId).Execute();
            foreach (var sheet in spreadsheet.Sheets)
            {
                if (sheet.Properties.Title == sheetName)
                {
                    return true;
                }
            }
            return false;
        }

        public void CreateSheet(string spreadsheetId, string sheetName)
        {
            var addSheetRequest = new AddSheetRequest
            {
                Properties = new SheetProperties
                {
                    Title = sheetName
                }
            };

            var request = new Request
            {
                AddSheet = addSheetRequest
            };

            var batchUpdateRequest = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request> { request }
            };

            service.Spreadsheets.BatchUpdate(batchUpdateRequest, spreadsheetId).Execute();
        }
    }
}
