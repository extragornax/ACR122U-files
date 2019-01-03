//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              DeviceProg.pas
//
//  Description:       This sample program outlines the steps on how to
//                     program device specific functions in ACR122
//
//  Author:	           Wazer Emmanuel R. Benal
//
//  Date:	             July 29, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

unit DeviceProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, AcsModule, ComCtrls, ExtCtrls;

const MAX_BUFFER_LEN    = 256;

type
  TfrmDevProg = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    btnInit: TButton;
    btnConnect: TButton;
    rbOutput: TRichEdit;
    btnGetFW: TButton;
    AntennaGroup: TGroupBox;
    rOn: TRadioButton;
    rOff: TRadioButton;
    btnSetAntenna: TButton;
    RedLEDGroup: TGroupBox;
    FinalRed: TGroupBox;
    rFinalRedOn: TRadioButton;
    rFinalRedOff: TRadioButton;
    StateRed: TGroupBox;
    rStateRedOn: TRadioButton;
    rStateRedOff: TRadioButton;
    BlinkRed: TGroupBox;
    rBlinkRedOn: TRadioButton;
    rBlinkRedOff: TRadioButton;
    RedBlinkMask: TGroupBox;
    rRedBlinkMaskOn: TRadioButton;
    rRedBlinkMaskOff: TRadioButton;
    GreenLEDGroup: TGroupBox;
    FinalGreen: TGroupBox;
    rFinalGreenOn: TRadioButton;
    rFinalGreenOff: TRadioButton;
    StateGreen: TGroupBox;
    rStateGreenOn: TRadioButton;
    rStateGreenOff: TRadioButton;
    BlinkGreen: TGroupBox;
    rBlinkGreenOn: TRadioButton;
    rBlinkGreenOff: TRadioButton;
    GreenBlinkMask: TGroupBox;
    rGreenBlinkMaskOn: TRadioButton;
    rGreenBlinkMaskOff: TRadioButton;
    BlinkDuration: TGroupBox;
    T1: TGroupBox;
    Label2: TLabel;
    tbT1: TEdit;
    Label3: TLabel;
    T2: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    tbT2: TEdit;
    Label6: TLabel;
    tbReps: TEdit;
    BuzzerLink: TGroupBox;
    rBuzzerOff: TRadioButton;
    rT1: TRadioButton;
    rT2: TRadioButton;
    rT1T2: TRadioButton;
    btnSetAll: TButton;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    btnStatus: TButton;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnGetFWClick(Sender: TObject);
    procedure btnSetAntennaClick(Sender: TObject);
    procedure tbT1KeyPress(Sender: TObject; var Key: Char);
    procedure tbT2KeyPress(Sender: TObject; var Key: Char);
    procedure tbRepsKeyPress(Sender: TObject; var Key: Char);
    procedure btnQuitClick(Sender: TObject);
    procedure btnSetAllClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure btnStatusClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmDevProg: TfrmDevProg;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;

implementation

{$R *.dfm}
procedure DisplayOut(output: String; mode: integer);
begin

  case mode of
    1: frmDevProg.rbOutput.SelAttributes.Color := clBlue;
    2: frmDevProg.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmDevProg.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmDevProg.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmDevProg.rbOutput.Lines.Add(output);
  frmDevProg.rbOutput.SelAttributes.Color := clBlack;
  frmDevProg.rbOutput.SetFocus;

end;

procedure ClearBuffers();
var index: integer;
begin

  for index := 0 to 262 do
    begin
      SendBuff[index] := $00;
      RecvBuff[index] := $00;
    end;

end;

function SendAPDU(): integer;
var index: integer; tempstr: String;
begin

  ioRequest.dwProtocol := dwActProtocol;
  ioRequest.cbPciLength := sizeof(SCARD_IO_REQUEST);

  tempstr := '';
  for index := 0 to SendLen - 1 do
    tempstr := tempstr + Format('%.02X ', [SendBuff[index]]);
  DisplayOut(tempstr,3);

  retCode := SCardTransmit(hCard,
                           @ioRequest,
                           @SendBuff,
                           SendLen,
                           Nil,
                           @RecvBuff,
                           @RecvLen);
  if retcode <> SCARD_S_SUCCESS then begin
    DisplayOut(GetScardErrMsg(retcode),2);
    SendAPDU := retcode;
    Exit;
  end;

  tempstr := '';
  for index := 0 to RecvLen - 1 do
    tempstr := tempstr + Format('%.02X ', [RecvBuff[index]]);

  DisplayOut(tempstr, 4);

  SendAPDU := retcode;

end;

procedure Initialize();
begin

  frmDevProg.btnInit.Enabled := true;
  frmDevProg.btnConnect.Enabled := false;
  frmDevProg.btnGetFW.Enabled := false;
  frmDevProg.AntennaGroup.Enabled := false;
  frmDevProg.RedLEDGroup.Enabled := false;
  frmDevProg.GreenLEDGroup.Enabled := false;
  frmDevProg.BlinkDuration.Enabled := false;
  frmDevProg.rOn.Checked := true;
  frmDevProg.rFinalRedOff.Checked := true;
  frmDevProg.rStateRedOff.Checked := true;
  frmDevProg.rBlinkRedOff.Checked := true;
  frmDevProg.rRedBlinkMaskOff.Checked := true;
  frmDevProg.rFinalGreenOff.Checked := true;
  frmDevProg.rStateGreenOff.Checked := true;
  frmDevProg.rBlinkGreenOff.Checked := true;
  frmDevProg.rGreenBlinkMaskOff.Checked := true;
  frmDevProg.rBuzzerOff.Checked := true;
  frmDevProg.tbT1.Text := '00';
  frmDevProg.tbT2.Text := '00';
  frmDevProg.tbReps.Text := '01';
  frmDevProg.cbReader.Text := '';
  frmDevProg.rbOutput.Text := '';
  DisplayOut('Program ready', 1 );

end;

procedure TfrmDevProg.btnInitClick(Sender: TObject);
var index: integer;
begin

  //Establish context
  retCode := SCardEstablishContext(SCARD_SCOPE_USER,
                                   nil,
                                   nil,
                                   @hContext);
  if retCode <> SCARD_S_SUCCESS then begin
    displayout(GetScardErrMsg(retcode),2);
    Exit;
  end ;

  //List PC/SC readers installed in the system
  BufferLen := MAX_BUFFER_LEN;
  retCode := SCardListReadersA(hContext,
                               nil,
                               @Buffer,
                               @BufferLen);
  if retCode <> SCARD_S_SUCCESS then begin
    DisplayOut(getscarderrmsg(retCode),2);
    Exit;
  end;

  btnInit.Enabled := false;
  btnConnect.Enabled := true;
  
  LoadListToControl(cbReader,@buffer,bufferLen);
  // Look for ACR128 PICC and make it the default reader in the combobox
  for index := 0 to cbReader.Items.Count-1 do begin
    cbReader.ItemIndex := index;
    if AnsiPos('ACR122U PICC', cbReader.Text) > 0 then
      Exit;
  end;
  cbReader.ItemIndex := 0;

end;

procedure TfrmDevProg.btnConnectClick(Sender: TObject);
begin

  //Connect to reader using a shared connection
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);

  if retcode <> SCARD_S_SUCCESS then begin
    displayout(GetScardErrMsg(retcode),2)
  end
  else begin
    displayout('Successful connection to ' + cbReader.Text, 1)
  end;

  frmDevProg.btnConnect.Enabled := false;
  frmDevProg.btnGetFW.Enabled := true;
  frmDevProg.AntennaGroup.Enabled := true;
  frmDevProg.RedLEDGroup.Enabled := true;
  frmDevProg.GreenLEDGroup.Enabled := true;
  frmDevProg.BlinkDuration.Enabled := true;
  
end;

procedure TfrmDevProg.btnGetFWClick(Sender: TObject);
var index: integer; tempstr: string;
begin

  //Get the firmware version of the reader
  ClearBuffers();

  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $48;
  SendBuff[3] := $00;
  SendBuff[4] := $00;

  SendLen := 5;
  RecvLen := 10;

  retcode := SendAPDU();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  //Interpret firmware data
  tempstr := 'Firmware Version: ';
  for index := 0 to RecvLen - 1 do
    tempstr := tempstr + Chr(RecvBuff[index]);

  DisplayOut(tempstr, 1);
  
end;

procedure TfrmDevProg.btnSetAntennaClick(Sender: TObject);
begin

  //Set antenna options
  ClearBuffers();

  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $04;
  SendBuff[5] := $D4;
  SendBuff[6] := $32;
  SendBuff[7] := $01;

  if rOn.Checked = true then begin
    SendBuff[8] := $01;
  end
  else begin
    SendBuff[8] := $00;
  end;

  SendLen := 9;
  RecvLen := 4;

  retcode := SendAPDU();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmDevProg.tbT1KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmDevProg.tbT2KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmDevProg.tbRepsKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmDevProg.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;
  
end;

procedure TfrmDevProg.btnSetAllClick(Sender: TObject);
begin

  //Validate inputs
  if tbT1.Text = '' then begin
    tbT1.SetFocus;
    Exit;
  end;

  if tbT2.Text = '' then begin
    tbT2.SetFocus;
    Exit;
  end;

  if tbReps.Text = '' then begin
    tbReps.SetFocus;
    Exit;
  end;

  if tbReps.Text = '00' then begin
    tbReps.Text := '01';
    tbReps.SetFocus;
    Exit;
  end;

  ClearBuffers();

  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $40;
  SendBuff[3] := $00;

  if rFinalRedOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $01;
  end;

  if rFinalGreenOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $02;
  end;

  if rStateRedOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $04;
  end;

  if rStateGreenOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $08;
  end;

  if rBlinkRedOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $10;
  end;

  if rBlinkGreenOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $20;
  end;

  if rRedBlinkMaskOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $40;
  end;

  if rGreenBlinkMaskOn.Checked = true then begin
    SendBuff[3] := SendBuff[3] or $80;
  end;

  SendBuff[4] := $40;
  SendBuff[5] := StrToInt('$' + tbT1.Text);
  SendBuff[6] := StrToInt('$' + tbT2.Text);
  SendBuff[7] := StrToInt('$' + tbReps.Text);

  if rBuzzerOff.Checked = true then begin
    SendBuff[8] := $00;
  end;

  if rT1.Checked = true then begin
    SendBuff[8] := $01;
  end;

  if rT2.Checked = true then begin
    SendBuff[8] := $02;
  end;

  if rT1T2.Checked = true then begin
    SendBuff[8] := $03;
  end;

  SendLen := 9;
  RecvLen := 2;

  retcode := SendAPDU();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmDevProg.btnClearClick(Sender: TObject);
begin

  rbOutput.Text := '';
  
end;

procedure TfrmDevProg.btnResetClick(Sender: TObject);
begin

  retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retcode := ScardReleaseContext(hContext);
  rbOutput.Text := '';
  Initialize();

end;

procedure TfrmDevProg.FormActivate(Sender: TObject);
begin

  Initialize();

end;

procedure TfrmDevProg.btnStatusClick(Sender: TObject);
var index: integer; tempstr: string;
begin

  ClearBuffers();
  //Get Status command
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $02;
  SendBuff[5] := $D4;
  SendBuff[6] := $04;

  SendLen := 7;
  RecvLen := 12;

  retcode := SendAPDU();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  for index := recvlen - 2 to RecvLen - 1 do
    tempstr := tempstr + Format('%.02X ', [RecvBuff[index]]);

  if (tempstr = 'D505280000809000') or (tempstr = 'D505000000809000') then begin
    //No tag is in the field
    DisplayOut('No tag is in the field ' + Format('%.02X',[RecvBuff[0]]), 1);
  end
  else begin
    //error code
    DisplayOut('Error Code: ' + Format('%.02X',[RecvBuff[2]]), 2);

    //Field indicates if an external RF field is present and detected
    //(Field=0x01 or not (Field 0x00)
    if RecvBuff[3] = $01 then begin
      DisplayOut('External RF field is Present and detected: ' + Format('%.02X',[RecvBuff[3]]), 4);
    end
    else begin
      DisplayOut('External RF field is NOT Present and NOT detected: ' + Format('%.02X',[RecvBuff[3]]), 4);
    end;

    //Number of targets currently controlled by the PN532 acting as initiator.The default value is 1
    DisplayOut('Number of Target: ' + Format('%.02X',[RecvBuff[4]]), 4);

    //Logical Number
    DisplayOut('Number of Target: ' + Format('%.02X',[RecvBuff[5]]), 4);

    //Bit rate in reception
    case RecvBuff[6] of
      $00: DisplayOut('Bit Rate in Reception: ' + Format('%.02X',[RecvBuff[6]]) + ' (106 kbps)', 4);
      $01: DisplayOut('Bit Rate in Reception: ' + Format('%.02X',[RecvBuff[6]]) + ' (212 kbps)', 4);
      $02: DisplayOut('Bit Rate in Reception: ' + Format('%.02X',[RecvBuff[6]]) + ' (424 kbps)', 4);
    end;

    //Bit rate in transmission
    case RecvBuff[7] of
      $00: DisplayOut('Bit Rate in Transmission: ' + Format('%.02X',[RecvBuff[7]]) + ' (106 kbps)', 4);
      $01: DisplayOut('Bit Rate in Transmission: ' + Format('%.02X',[RecvBuff[7]]) + ' (212 kbps)', 4);
      $02: DisplayOut('Bit Rate in Transmission: ' + Format('%.02X',[RecvBuff[7]]) + ' (424 kbps)', 4);
    end;

    case RecvBuff[8] of
      $00: DisplayOut('Modulation Type: ' + Format('%.02X',[RecvBuff[8]]) + ' (ISO14443 or MiFare)', 4);
      $01: DisplayOut('Modulation Type: ' + Format('%.02X',[RecvBuff[8]]) + ' (Active Mode)', 4);
      $02: DisplayOut('Modulation Type: ' + Format('%.02X',[RecvBuff[8]]) + ' (Innovision Jewel Tag)', 4);
      $10: DisplayOut('Modulation Type: ' + Format('%.02X',[RecvBuff[8]]) + ' (Felica)', 4);
    end;

  end;

end;

end.
