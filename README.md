Extra Actions
=============

A collection of helpful Workflow Actions for Rock RMS.

## Included Actions:
- [Run Powershell](#run-powershell)
- [Redirect to Page](#redirect-to-page)
- [Show HTML](#show-html)

## Run Powershell
The *Run Powershell* action lets you run a powershell script on the server, format the result, and store it in an attribute. You can use it to do almost anything - do more advanced math, get the last time the server was restarted, look through and change server folders and files, remotely add a new account in Office 365. Your only limit is your imagination!

![Screenshot](https://newpointe.blob.core.windows.net/newpointe-webassets/upload/0d5f2adb24e14a8ba090a2fd66dd26df_RunPowershell.png)

### Examples:

**Math:**
```powershell
3 + (4 * [math]::pow(5,6))
```
```
62503
```
**Text:**
```powershell
"Powershell is awesome!".replace("Powershell", "Rock")
```
```
Rock is awesome!
```
&nbsp;
```powershell
"Hello {{ CurrentPerson.NickName }}! It's " + (Get-Date -Format "t")
```
```
Hello John! It's 11:45 AM
```
**And more:**
```powershell
$OS=(Get-WmiObject Win32_OperatingSystem)
"The server was last restarted at: " + $OS.ConvertToDateTime($OS.LastBootUpTime).ToString();
```
```
The server was last restarted at: 9/9/2016 11:06:20 AM
```
&nbsp;
```powershell
$rockRoot = ((Get-Location).Path -replace "(?<=RockWeb).*", "")
Get-ChildItem "$rockRoot\Themes"
```
```
CheckinAdventureKids
CheckinBlueCrystal
CheckinPark
DashboardStark
Flat
KioskStark
Rock
Stark
```
&nbsp;
```powershell
$User = "john@yourchurch.org"
$PWord = ConvertTo-SecureString -String <EncryptedPassword>

$UserCredential = New-Object –TypeName "System.Management.Automation.PSCredential" –ArgumentList $User, $PWord
$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri https://outlook.office365.com/powershell-liveid/ -Credential $UserCredential -Authentication Basic -AllowRedirection
Import-PSSession $Session

Get-Mailbox "john@yourchurch.org"

Remove-PSSession $Session
```
```
tmp_jo5hefjt.y0t
John Smith
```


## Redirect to Page
The *Redirect to Page* action will redirect the user to the given URL (If the workflow is being run in the background it does nothing).   

Also lets you set how the workflow should continue:  
**`Always Continue`** - The action will complete and the workflow will continue processing even if it was ran in the background.  
**`Only continue on redirect`** - The action will not complete and the workflow will not continue processing until it can redirect someone.  
**`Never continue`** - The action will never complete and the workflow will not continue processing untill a filter is used to skip it.  

![Screenshot](https://newpointe.blob.core.windows.net/newpointe-webassets/upload/87c0ce510fd8453e93113abb101b6b95_RedirectToUrl.png)


## Show HTML
The *Workflow Entry Show HTML* action will show the given HTML in the WorkflowEntry block (If the workflow is being run in the background it does nothing).   

Also lets you set if you want to hide the built-in status message.  

![Screenshot](https://newpointe.blob.core.windows.net/newpointe-webassets/upload/d3e05367ae434fb8b13a5c97bac7ad74_ShowHtml.png)

