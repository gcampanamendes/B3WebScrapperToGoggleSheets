# B3WebScrapperToGoogleSheets

## Welcome!
Here you'll find all the code and setting instructions for B3WebScrapperToGoogleSheets tool.

## Instructions

### Set Google Cloud
1. **Create a Google Cloud Project**:
   - Go to the [Google Cloud Console](https://console.cloud.google.com/).
   - Click on the project drop-down menu at the top of the page and select "New Project".
   - Enter a project name and click "Create".

2. **Enable the Google Sheets API**:
   - In the Google Cloud Console, go to the [API Library](https://console.cloud.google.com/apis/library).
   - Search for "Google Sheets API" and click on it.
   - Click "Enable" to enable the API for your project.

3. **Create a Service Account**:
   - In the Google Cloud Console, go to the [IAM & Admin](https://console.cloud.google.com/iam-admin) section.
   - Click on "Service Accounts" in the left-hand menu.
   - Click on the "Create Service Account" button at the top of the page.
   - Enter a service account name, ID, and description, then click "Create and Continue".

4. **Set Permissions for the Service Account**:
   - In the "Grant this service account access to project" section, select the roles you want to assign to the service account. For example, you might choose "Editor" or "Viewer" depending on your needs.
   - Click "Continue".

5. **Grant Users Access to the Service Account**:
   - In the "Grant users access to this service account" section, you can add users who can manage the service account.
   - Click "Done".

6. **Create and Download the Key File**:
   - After creating the service account, click on the service account you just created.
   - Go to the "Keys" tab.
   - Click on "Add Key" and select "Create New Key".
   - Choose the key type (JSON) and click "Create".
   - The key file will be downloaded to your computer. Save this file securely, as it contains the credentials needed to authenticate as the service account.

### Set [config.json](./config.json) file
The `config.json` file should be located inside the `config` folder in the root directory of the project.

`[app-directory]\config\config.json`

#### Web Configuration
1. **Getting XPath**:
   - Open the target webpage in your browser.
   - Right-click on the element you want to scrape and select "Inspect" to open the Developer Tools.
   - Right-click on the highlighted HTML element in the Developer Tools and select "Copy" > "Copy XPath" or "Copy Full XPath".
   - Paste the copied XPath into the respective field in the `config.json` file under `"web": { "fii": { "xpath": { "attribute": "..." } } }` or `"web": { "stock": { "xpath": { "attribute": "..." } } }`.

2. **Adding Website URL**:
   - Add the base URL of the website to the `config.json` file under `"web": { "fii": { "url": "https://www.fundsexplorer.com.br/funds/" } }` or `"web": { "stock": { "url": "https://www.analisedeacoes.com/acoes/" } }`.
   - Note: The ticker will be concatenated to the end of the URL, so the final URL will look like `https://www.fundsexplorer.com.br/funds/{ticker}` or `https://www.analisedeacoes.com/acoes/{ticker}`.

#### Sheets Configuration
1. **Credential JSON File Path**:
   - Provide the path to the downloaded credentials JSON file in the `config.json` file under `"sheets": { "credentialsJsonFilePath": "C:\\temp\\your-google-credential-files-here.json" }`.

2. **Spreadsheet ID**:
   - Provide the ID of the Google Sheets spreadsheet in the `config.json` file under `"sheets": { "spreadsheetId": "your-google-sheets-id-here" }`.

3. **Sheet Names**:
   - Provide the names of the sheets for Fii and Stock in the `config.json` file under `"sheets": { "sheetNames": { "fii": "your-fii-sheet-name", "stock": "your-stock-sheet-name" } }`.

#### Portfolio Configuration
1. **Fii Portfolio**:
   - List the tickers for the Fii portfolio in the `config.json` file under `"portfolios": { "fii": ["MXRF11", "XPML11", ..., ... , ...] }`.

2. **Stock Portfolio**:
   - List the tickers for the Stock portfolio in the `config.json` file under `"portfolios": { "stock": ["BBAS3", "BBDC4", ..., ... , ...] }`.

### [config.json](./config.json) file

```json
{
  "config": {
    "web": {
		"fii": {
			"xpath": {
				"name": "//*[@id=\"carbon_fields_fiis_header-2\"]/div/div/div[1]/p",
				"ticker": "//*[@id=\"carbon_fields_fiis_header-2\"]/div/div/div[1]/div[1]/p",
				"segment": "//*[@id=\"carbon_fields_fiis_basic_informations-2\"]/div/div/div[6]/p[2]/b",
				"anbima": "//*[@id=\"carbon_fields_fiis_basic_informations-2\"]/div/div/div[7]/p[2]/b",
				"management": "//*[@id=\"carbon_fields_fiis_basic_informations-2\"]/div/div/div[10]/p[2]/b",
				"price": "//*[@id=\"carbon_fields_fiis_header-2\"]/div/div/div[1]/div[1]/p",
				"dy30d": "//*[@id=\"indicators\"]/div[2]/p[2]/b",
				"dy12m": "//*[@id=\"indicators\"]/div[3]/p[2]/b/text()",
				"dy5y": "",
				"payoutPerShare12m": "//*[@id=\"indicators\"]/div[3]/p[2]/b",
				"payoutLast": "//*[@id=\"indicators\"]/div[2]/p[2]/b",
				"vp": "//*[@id=\"indicators\"]/div[5]/p[2]/b/text()",
				"pvp": "//*[@id=\"indicators\"]/div[7]/p[2]/b",
				"vacancy": "",
				"variation1d": "//*[@id=\"carbon_fields_fiis_header-2\"]/div/div/div[1]/div[1]/span",
				"variation30d": "//*[@id=\"indicators\"]/div[6]/p[2]/b/text()",
				"variation12m": "//*[@id=\"carbon_fields_fiis_quotations-2\"]/div/div[1]/div[2]/div[4]/p[1]/text()",
				"dailyLiquidity": "//*[@id=\"indicators\"]/div[1]/p[2]/b",
				"quotas": "//*[@id=\"carbon_fields_fiis_basic_informations-2\"]/div/div/div[8]/p[2]/b",
				"holders": "//*[@id=\"comparadorAtivos\"]/div[2]/div[1]/div/div[2]/ul/li[6]/text()",
				"networth": "//*[@id=\"comparadorAtivos\"]/div[2]/div[1]/div/div[2]/ul/li[1]/text()"
        },
			"url": "https://www.fundsexplorer.com.br/funds/"
      },
		"stock": {
			"xpath": {
				"name": "/html/body/div/div[1]/section/main/header/div/div/div/div[1]/h2/text()",
				"ticker": "/html/body/div/div[1]/section/main/header/div/div/div/div[1]/h1",
				"sector": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[7]/div/div[2]/div/ul/li[1]/p/a",
				"price": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[1]/div/div[2]/div/section[1]/ul/li[1]/p/span",
				"pl": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[2]/li[2]/p/span/text()",
				"pvp": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[2]/li[3]/p/span/text()",
				"dy": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[2]/li[1]/p/span/text()",
				"lpa": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[2]/li[4]/p/span/text()",
				"vpa": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[2]/li[5]/p/span/text()",
				"variation12m": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[1]/div/div[2]/div/section[1]/ul/li[4]/p/span",
				"variation1m": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[1]/div/div[2]/div/section[1]/ul/li[3]/p/span",
				"variation7d": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[1]/div/div[2]/div/section[1]/ul/li[2]/p/span",
				"tagAlong": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[7]/div/div[2]/div/ul/li[6]/p/span",
				"roe": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[3]/li[3]/p/span",
				"roic": "/html/body/div/div[1]/section/main/div[1]/div/div/div[2]/section[2]/div[1]/div[2]/div/ul[3]/li[4]/p/span/text()"
			},
			"url": "https://www.analisedeacoes.com/acoes/"
		}
    },
    "sheets": {
		"credentialsJsonFilePath": "C:\\temp\\your-google-credential-files-here.json",
		"spreadsheetId": "your-google-sheets-id-here",
		"sheetNames": {
				"fii": "your-fii-sheet-name",
				"stock": "your-stock-sheet-name"
			}
    },
	"portfolios": {
		"fii": [ "MXRF11", "XPML11" ],
		"stock": [ "BBAS3", "BBDC4" ]
	}
  }
}
