; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Crud Creator"
#define MyAppVersion "5.4.5.7"
#define MyAppPublisher "SunSale System"
#define MyAppURL "http://www.sunsalesystem.com.br/"
#define MyAppExeName "CrudForms.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".exe"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B7DC643A-D496-474D-9C34-064871102340}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=mysetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "portuguese"; MessagesFile: "compiler:Languages\Portuguese.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Instalador\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\BouncyCastle.Crypto.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\BouncyCastle.Crypto.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\copyFiles.bat"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\CrudForms.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\CrudForms.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\CrudForms.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Google.Protobuf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Google.Protobuf.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Google.Protobuf.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\K4os.Compression.LZ4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\K4os.Compression.LZ4.Streams.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\K4os.Compression.LZ4.Streams.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\K4os.Compression.LZ4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\K4os.Hash.xxHash.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\K4os.Hash.xxHash.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Microsoft.Bcl.AsyncInterfaces.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Microsoft.Bcl.AsyncInterfaces.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Microsoft.Bcl.HashCode.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Microsoft.Bcl.HashCode.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\MySql.Data.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Npgsql.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Npgsql.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\RestSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\RestSharp.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\SQLite.Interop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Buffers.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Data.SQLite.EF6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Data.SQLite.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Data.SQLite.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Diagnostics.DiagnosticSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Diagnostics.DiagnosticSource.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Memory.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Numerics.Vectors.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Runtime.CompilerServices.Unsafe.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Text.Encodings.Web.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Text.Encodings.Web.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Text.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Text.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Threading.Channels.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Threading.Channels.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Threading.Tasks.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.Threading.Tasks.Extensions.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.ValueTuple.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\System.ValueTuple.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Utils.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Utils.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\Utils.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\ZstdNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Instalador\x86\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

