; ProjectSWG Launcher Installer
 
; Version 1.0.1.17
 
; To test version system, download http://lewaverunner.x10.mx/PSWG_LauncherInstall_OLDINSTALLER.exe  ( version 1.0.1.15, only difference is that it shows a messagebox saying "MessageBox". See Function .onIt )
 
;--------------------------------
;Includes
 
  !include "MUI2.nsh"
  !include "DotNetSearch.nsh" ; Custom, See http://nsis.sourceforge.net/How_to_insure_a_required_version_of_.NETFramework_is_installed
  !include "VersionComplete.nsh" ; Custom, See http://nsis.sourceforge.net/VersionCompleteXXXX
  !include "nsDialogs.nsh"
  !include "winmessages.nsh"
  !include "logiclib.nsh"

;--- macro stuff
!macro NSD_SetUserData hwnd data
  nsDialogs::SetUserData ${hwnd} ${data}
!macroend
!define NSD_SetUserData `!insertmacro NSD_SetUserData`
 
!macro NSD_GetUserData hwnd outvar
  nsDialogs::GetUserData ${hwnd}
  Pop ${outvar}
!macroend
!define NSD_GetUserData `!insertmacro NSD_GetUserData`

;--------------------------------
;General
 
  ;Name and file
  Name "ProjectSWG Launcher"
  OutFile "PSWGInstaller.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\ProjectSWG"
 
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\ProjectSWG" "Location"
 
  ;Request application privileges for Windows Vista
  RequestExecutionLevel highest

 
;--------------------------------
;Interface Settings
 
;BrandingText "ProjectSWG Installer created with Nullsoft Install System"
  !define MUI_ICON "ProjectSWG Launcher.ico"
  !define MUI_ABORTWARNING
  !define MUI_WELCOMEFINISHPAGE_BITMAP "projectswg_wf.bmp"
 
;  !define MUI_FINISHPAGE_RUN
;  !define MUI_FINISHPAGE_RUN_TEXT "Run ProjectSWG Launcher after closing"
;  !define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLauncher"
 
;--------------------------------
;Version Settings & Build Details
 
; This will automatically increment each build version of the installer.
; Requires a installer.ini file and installer-revision.txt file in order to build. Isn't needed for runtime.
; It will also auto-create installer_version.ini which should be uploaded to server.
 
; Below are the current values in said files. Will update accordingly.
 
;=======version.ini======= Main version builds.
;version=1.0.1
 
;=======installer-revision.txt======= This just defines the last number of each build.
;11
 
!define /file REVISION_LAST installer-revision.txt
; ${REVISION_LAST} returns # inside txt
!define /math REVISION ${REVISION_LAST} + 1
; ${REVISION} returns {REVISION_LAST} incremented by 1 number
!delfile installer-revision.txt
!appendfile installer-revision.txt ${REVISION}
; Makes new file and enters in ${REVISION} number
!searchparse /file version.ini `version=` VERSION_SHORT  ; version.ini : verson=1.0.1
; ${VERSION_SHORT} returns 1.0.1
${VersionCompleteXXXXRevision} ${VERSION_SHORT} VERSION_1 ${REVISION}
 
VIProductVersion ${VERSION_1}
VIAddVersionKey FileVersion ${VERSION_1}
VIAddVersionKey ProductVersion ${VERSION_1}
 
VIAddVersionKey ProductName "PSWGLauncher Installer"
VIAddVersionKey FileDescription "PSWGLauncher Installer"

var dialog
var hwnd
var admin
 
;--------------------------------
;Pages
 
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "License.txt"
  !insertmacro MUI_PAGE_COMPONENTS
;  Page Custom DialogAdmin DialogAdminContinue
  Page Custom DialogAdmin
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH
 
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_COMPONENTS
  !insertmacro MUI_UNPAGE_INSTFILES
 
 
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer - Required
 
Section "ProjectSWG Launcher" SecPSWGInstall
 
  SetOutPath "$INSTDIR"
  File "ProjectSWG Launcher.exe"
 
  ;Store installation folder
  WriteRegStr HKCU "Software\ProjectSWG" "Location" $INSTDIR
  WriteRegDWord HKCU "Software\ProjectSWG" "RunAsBehaviour" $admin
 
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
SectionEnd

SectionGroup /e "Create Shortcuts"
  Section "on Desktop" SectionX
    SetShellVarContext current
    CreateShortCut "$DESKTOP\ProjectSWG Launcher.lnk" "$INSTDIR\ProjectSWG Launcher.exe"
  SectionEnd
  Section "in Start Menu" SectionY
    CreateDirectory "$SMPROGRAMS\Project SWG"
    CreateShortCut "$SMPROGRAMS\Project SWG\ProjectSWG Launcher.lnk" "$INSTDIR\ProjectSWG Launcher.exe"
    CreateShortCut "$SMPROGRAMS\Project SWG\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
  SectionEnd
SectionGroupEnd

;--------------------------------
;Installer - Skins  << For when skins are implemented, this will only write text string to Registry for now >>
 
; NOTE: Check for user selecting more than 1 box not implemented.
;SectionGroup "Launcher Skin" SecGroupSkins
 
        ;Section "Imperial Skin" SecOptionalSkinImp
                ; Write the string name
                ;WriteRegStr HKCU "Software\ProjectSWG" "Skin" "Imperial"
        ;SectionEnd
       
        ;Section "Rebel Skin" SecOptionalSkinReb
                ;WriteRegStr HKCU "Software\ProjectSWG" "Skin" "Rebel"
        ;SectionEnd
       
;SectionGroupEnd
 
;--------------------------------
;Descriptions
 
  ;Language strings
  LangString DESC_SecPSWGINSTALL ${LANG_ENGLISH} "Launcher for playing and connecting to ProjectSWG. Required."
 
  ; Text for the description when installing skins.
  ;LangString DESC_SecGroupSkins ${LANG_ENGLISH} "Use a different skin for the launcher besides the default one. Can be changed in options."
        ;LangString DESC_SecOptionalSkinImp ${LANG_ENGLISH} "Optional Imperial Skin for the launcher."
        ;LangString DESC_SecOptionalSkinReb ${LANG_ENGLISH} "Optional Rebellion Skin for the launcher."
 
 
  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    ;!insertmacro MUI_DESCRIPTION_TEXT ${SecPSWGInstall} $(DESC_SecPSWGInstall)
        ;!insertmacro MUI_DESCRIPTION_TEXT ${SecGroupSkins} $(DESC_SecGroupSkins)
                ;!insertmacro MUI_DESCRIPTION_TEXT ${SecOptionalSkinImp} $(DESC_SecOptionalSkinImp)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END
 
;--------------------------------
;Uninstaller Section
 
Section "Uninstall"
 
  Delete "$INSTDIR\ProjectSWG Launcher.exe"
  Delete "$INSTDIR\Uninstall.exe"

  Delete "$DESKTOP\ProjectSWG Launcher.lnk"
  Delete "$SMPROGRAMS\Project SWG\ProjectSWG Launcher.lnk"
  Delete "$SMPROGRAMS\Project SWG\Uninstall.lnk"
  RMDir  "$SMPROGRAMS\Project SWG"
 
  ;RMDir /r "$INSTDIR"
 
  DeleteRegKey HKCU "Software\ProjectSWG"
 
SectionEnd
 
;--------------------------------
; Functions ( Keep custom functions on bottom, otherwise error on compile (functions that don't have "." before them) )
 
Function .onInit
  SetRegView 64
 
  SectionSetFlags ${SecPSWGInstall} 17 ; Make the launcher install option read only (16) and selected (1)
  ;SectionSetFlags ${SecGroupSkins} 34 ; Make skin group unchecked by default (2) and group expanded (32)
       
  !insertmacro DotNetSearch 4 0 "" "ABORT" "" ; Checks to make sure the user has .NET 4.0 before installing.

  ReadRegDWord $admin HKCU "Software\ProjectSWG" "RunAsBehaviour"

FunctionEnd
 
Function .onVerifyInstDir
                ; Prevent user from installing to SWG folder. Called everytime user changes install directory.
        ${If} $INSTDIR == "$PROGRAMFILES\StarWarsGalaxies"
                MessageBox MB_OK|MB_ICONEXCLAMATION "Can't install to SWG folder, please select a different directory."
                Abort
        ${EndIf}
       
        ${If} $INSTDIR == "$PROGRAMFILES\Steam\SteamApps\common\StarWarsGalaxies"
                MessageBox MB_OK|MB_ICONEXCLAMATION "Can't install to SWG folder, please select a different directory."
                Abort
        ${EndIf}
FunctionEnd
 
;Function .onInstSuccess
;        MessageBox MB_OK|MB_USERICON "Launcher installed successfully!"
;FunctionEnd
 

Function DialogAdmin
	nsDialogs::Create 1018
		Pop $dialog
	${NSD_CreateRadioButton} 0 0 80% 12% "Do not request admin privileges. (Default)"
	  Pop $hwnd
	  ${NSD_AddStyle} $hwnd ${WS_GROUP}
	  ${NSD_SetUserData} $hwnd 0
	  ${NSD_OnClick} $hwnd RadioClick
          ${If} $admin != 1
            ${NSD_Check} $hwnd
          ${EndIf}
	${NSD_CreateRadioButton} 0 12% 80% 12% "Request admin privileges."
	  Pop $hwnd
	  ${NSD_SetUserData} $hwnd 1
	  ${NSD_OnClick} $hwnd RadioClick
          ${If} $admin == 1
            ${NSD_Check} $hwnd
          ${EndIf}
	nsDialogs::Show
FunctionEnd

Function RadioClick
	Pop $hwnd
	${NSD_GetUserData} $hwnd $admin
FunctionEnd
 
;Function DialogAdminContinue
;	${If} $admin == ""
;	    MessageBox MB_OK "Please choose."
;	    Abort
;	${EndIf}
;FunctionEnd


