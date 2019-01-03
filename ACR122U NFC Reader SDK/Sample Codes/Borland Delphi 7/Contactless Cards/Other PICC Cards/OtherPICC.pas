//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              OtherPICC.pas
//
//  Description:       This sample program outlines the steps on how to
//                     transact with Other PICC cards using ACR122
//
//  Author:	           Wazer Emmanuel R. Benal
//
//  Date:	             July 28, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

unit OtherPICC;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, AcsModule;

const MAX_BUFFER_LEN    = 256;

type
  TfrmPICC = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    btnInit: TButton;
    btnConnect: TButton;
    DataGroup: TGroupBox;
    check1: TCheckBox;
    btnGetData: TButton;
    SendGroup: TGroupBox;
    tbCLA: TEdit;
    tbINS: TEdit;
    tbP1: TEdit;
    tbP2: TEdit;
    tbLc: TEdit;
    tbLe: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    tbData: TEdit;
    btnSend: TButton;
    rbOutput: TRichEdit;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnGetDataClick(Sender: TObject);
    procedure btnSendClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
    procedure btnQuitClick(Sender: TObject);
    procedure tbCLAKeyPress(Sender: TObject; var Key: Char);
    procedure tbINSKeyPress(Sender: TObject; var Key: Char);
    procedure tbP1KeyPress(Sender: TObject; var Key: Char);
    procedure tbP2KeyPress(Sender: TObject; var Key: Char);
    procedure tbLcKeyPress(Sender: TObject; var Key: Char);
    procedure tbLeKeyPress(Sender: TObject; var Key: Char);
    procedure FormActivate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmPICC: TfrmPICC;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  validATS    : Boolean;

implementation

{$R *.dfm}

procedure DisplayOut(output: String; mode: integer);
begin

  case mode of
    1: frmPICC.rbOutput.SelAttributes.Color := clBlue;
    2: frmPICC.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmPICC.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmPICC.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmPICC.rbOutput.Lines.Add(output);
  frmPICC.rbOutput.SelAttributes.Color := clBlack;
  frmPICC.rbOutput.SetFocus;

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

function SendAPDU(mode: integer): integer;
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
  case mode of
  1:  begin
      for index := 0 to RecvLen - 1 do
        tempstr := tempstr + Format('%.02X ', [RecvBuff[index]]);
      DisplayOut(tempstr, 4);
      end;
  2:  begin      // Interpret SW1/SW2
       for index := (RecvLen-2) to (RecvLen-1) do
         tempstr := tempstr + Format('%.02X', [(RecvBuff[index])]);
       if (Trim(tempstr) = '6A81') then begin
         DisplayOut('The function is not supported.',2);
         SendAPDU := retCode;
         Exit;
       end;
       if (Trim(tempstr) = '6300') then begin
         DisplayOut('The operation failed.',2);
         SendAPDU := retCode;
         Exit;
       end;
       validATS := True;
       end;
  end;

  SendAPDU := retcode;

end;

function TrimInput(TrimType: integer; StrIn: string): string;
var indx: integer;
    tmpStr: String;
begin
  tmpStr := '';
  StrIn := Trim(StrIn);
  case TrimType of
    0: begin
       for indx := 1 to length(StrIn) do
         if ((StrIn[indx] <> chr(13)) and (StrIn[indx] <> chr(10))) then
           tmpStr := tmpStr + StrIn[indx];
       end;
    1: begin
       for indx := 1 to length(StrIn) do
         if StrIn[indx] <> ' ' then
           tmpStr := tmpStr + StrIn[indx];
       end;
  end;
  TrimInput := tmpStr;
end;

procedure Initialize();
begin

  frmPICC.btnInit.Enabled := true;
  frmPICC.btnConnect.Enabled := false;
  frmPICC.DataGroup.Enabled := false;
  frmPICC.SendGroup.Enabled := false;
  frmPICC.cbReader.Text := '';
  frmPICC.check1.Checked := false;
  frmPICC.tbCLA.Text := '';
  frmPICC.tbINS.Text := '';
  frmPICC.tbP1.Text := '';
  frmPICC.tbP2.Text := '';
  frmPICC.tbLc.Text := '';
  frmPICC.tbLe.Text := '';
  frmPICC.tbData.Text := '';
  frmPICC.rbOutput.Text := '';
  DisplayOut('Program ready', 1);

end;

procedure TfrmPICC.btnInitClick(Sender: TObject);
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

procedure TfrmPICC.btnConnectClick(Sender: TObject);
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

  frmPICC.btnConnect.Enabled := false;
  frmPICC.DataGroup.Enabled := true;
  frmPICC.SendGroup.Enabled := true;

end;

procedure TfrmPICC.btnGetDataClick(Sender: TObject);
var index: integer; tempstr: string;
begin

  validATS := False;

  ClearBuffers();
  //Get Data command
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $CA;                             // INS
  if check1.Checked then begin
    SendBuff[2] := $01;                           // P1 : ISO 14443 A Card
  end
  else begin
    SendBuff[2] := $00;                           // P1 : Other cards
  SendBuff[3] := $00;                             // P2
  SendBuff[4] := $00;                             // Le : Full Length

  SendLen := SendBuff[4] + 5;
  RecvLen := $FF;
  end;

  retCode := SendAPDU(2);
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  // Interpret and display return values
  if validATS then begin
    if check1.Checked then begin
      tempstr := 'UID :';
    end
    else begin
      tempstr := 'ATS :';
    end;

    for index := 0 to (RecvLen -3 ) do
      tempstr := tempstr + Format('%.2X ', [(RecvBuff[index])]);
    DisplayOut(Trim(tempstr), 4);
  end;

end;

procedure TfrmPICC.btnSendClick(Sender: TObject);
var tmpData: string; directCmd: Boolean; index: integer;
begin

  directCmd := True;

  // Validate inputs

  if tbCLA.Text = '' then begin
    tbCLA.Text := '00';
    tbCLA.SetFocus;
    Exit;
  end;

  tmpData := '';

  ClearBuffers();
  SendBuff[0] := StrToInt('$'+ tbCLA.Text);        // CLA
  if tbINS.Text <> '' then
    SendBuff[1] := StrToInt('$'+ tbINS.Text);      // INS
  if tbP1.Text <> '' then
    directCmd := False;
  if not directCmd then begin
    SendBuff[2] := StrToInt('$'+ tbP1.Text);       // P1
    if tbP2.Text = '' then begin
      tbP2.Text := '00';                           // P2 : Ask user to confirm
      tbP2.SetFocus;
      Exit;
    end
    else
      SendBuff[3] := StrToInt('$'+ tbP2.Text);     // P2
    if tbLc.Text <> '' then begin
      SendBuff[4] := StrToInt('$'+ tbLc.Text);     // Lc
      if SendBuff[4] > 0 then begin               // Process Data In if Lc > 0
        tmpData := TrimInput(0, tbData.Text);
        tmpData := TrimInput(1, tmpData);
        if SendBuff[4] > (Length(tmpData) div 2) then begin  // Check if Data In is
          tbData.SetFocus;                                    // consistent with Lc value
          Exit;
        end;
        for index :=0 to SendBuff[4]-1 do
          SendBuff[index+5] := StrToInt('$' + copy(tmpData,(index*2+1),2)); // Format Data In
        if tbLe.Text <> '' then
          SendBuff[SendBuff[4]+5] := StrToInt('$'+ tbLe.Text);             // Le
      end
      else
        if tbLe.Text <> '' then
          SendBuff[5] := StrToInt('$'+ tbLe.Text);                // Le

    end
    else
      if tbLe.Text <> '' then
        SendBuff[4] := StrToInt('$'+ tbLe.Text);                // Le
  end;

  if directCmd then begin
    if tbINS.Text = '' then
      SendLen := $01
    else
      SendLen := $02;
  end
  else
    if tbLc.Text = '' then begin
      if tbLe.Text <> '' then
        SendLen := 5
      else
        SendLen := 4;
    end
    else
      if tbLe.Text = '' then
        SendLen := SendBuff[4] + 5
      else
        SendLen := SendBuff[4] + 6;
  RecvLen := $FF;

  retCode := SendAPDU(1);
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TfrmPICC.btnClearClick(Sender: TObject);
begin

  rbOutput.Text := '';
  
end;

procedure TfrmPICC.btnResetClick(Sender: TObject);
begin

  retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retcode := ScardReleaseContext(hContext);
  rbOutput.Text := '';
  Initialize();

end;

procedure TfrmPICC.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;
  
end;

procedure TfrmPICC.tbCLAKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmPICC.tbINSKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmPICC.tbP1KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmPICC.tbP2KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmPICC.tbLcKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmPICC.tbLeKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmPICC.FormActivate(Sender: TObject);
begin

  Initialize();

end;

end.
