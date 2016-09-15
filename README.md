Extra Actions
=============

A collection of helpful Workflow Actions for Rock RMS.

## Included Actions:
- [Run Powershell](#run-powershell)

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