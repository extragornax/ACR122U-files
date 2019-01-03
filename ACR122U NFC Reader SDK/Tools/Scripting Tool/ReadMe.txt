Readme for the ACR122U PC/SC Scripting Tool
Copyright (c) 2008, Advanced Card Systems Ltd.

Contents
----------------

   1. Release notes
   2. Installation
   3. Support

1. Release notes
----------------

ACR122PCSCScriptingTool.exe	v1.0.0.0 (08/21/2008)
ACR122PCSCScriptingTool98.exe	v1.0.0.0 (08/21/2008)

Please note that this program is a demo version only and should only be used as such. 
Since this is a demo only, we cannot take any responsibility for problems caused by mis-use of this program.

2. Usage
----------------

The ACR122U PC/SC Scripting Tool can be used to demonstrate card polling and APDU commands through scripts. 
The program is designed to be used with the ACR122 NFC Contactless Smart Card Reader.

2 versions for different operating systems are included:
- For Windows 98 or ME, please use ACR122PCSCScriptingTool98.exe
- For Windows 2000 and up, ACR122PCSCScriptingTool.exe can be used.

The program includes 4 scripts that can be loaded when either a:
- Mifare UltraLight;
- Mifare 1K;
- Mifare 4K;
- Topaz;
card is placed on the reader.

The cards listed below will be recognized by the polling command as well and displayed in the "Card" section of the program. 
Furthermore, if the below mentioned scripts are present in the "Scripts" sub-folder, the "Load"-button will be enabled as well:
  Card			Script name
  -----------		-----------
  Mifare Ultralight	UltraLight.txt
  MIFARE 1K		Mifare1K.txt
  MIFARE 4K		Mifare4K.txt
  MIFARE MINI		MifareMini.txt
  MIFARE DESFIRE	MifareDesFire.txt
  TOPAZ			Topaz.txt
  ISO/IEC14443-4A	OtherIsoA.txt
  ISO/IEC14443-4B	OtherIsoB.txt
  FELICA		Felica.txt

Usage of the program:
1) Plugin an ACR122 NFC reader and install the driver.
2) From the program, Select the listed reader (ACS ACR122U PICC Interface 0 or ACS ACR122 0)
4) Place a contactless card on the reader.
5) Press "Connect" to connect to the card (protocol T=1 is required)
Note: If one of the above cards is placed on the reader (and a script is present), 
      press "Load" to load the script.
5) Press "Execute" to execute the script.

Note: it is also possible to write and execute other scripts in the program, 
either by typing in the Script textbox or by pasting the commands from a text-file from e.g. notepad.

3. Support
----------------

In case of problems, please contact ACS through:
web site: http://www.acs.com.hk
email: info@acs.com.hk
tel: (852) 2796-7873


-----------------------------------------------------------------


Copyright 
Copyright by Advanced Card Systems Ltd. (ACS). No part of this document may be reproduced or transmitted in any from without the expressed, written permission of ACS. 

Notice 
Due to rapid change in technology, some of specifications mentioned in this publication are subject to change without notice. Information furnished is believed to be accurate and reliable. ACS assumes no responsibility for any errors or omissions, which may appear in this document. 

