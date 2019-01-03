//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              MifareProg.pas
//
//  Description:       This sample program outlines the steps on how to
//                     transact with MiFare cards using ACR122
//
//  Author:            Wazer Emmanuel R. Benal
//
//  Date:              July 23, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

unit MiFareProg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, AcsModule, StdCtrls, ComCtrls;

const MAX_BUFFER_LEN    = 256;

type
  TfrmMifare = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    rbOutput: TRichEdit;
    btnInit: TButton;
    btnConnect: TButton;
    LoadGroup: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    tbKeyNum: TEdit;
    tbKeyVal1: TEdit;
    tbKeyVal2: TEdit;
    tbKeyVal3: TEdit;
    tbKeyVal4: TEdit;
    tbKeyVal5: TEdit;
    tbKeyVal6: TEdit;
    btnLoad: TButton;
    AuthenGroup: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    KeyGroup: TGroupBox;
    rKeyA: TRadioButton;
    rKeyB: TRadioButton;
    tbBlockNum: TEdit;
    btnAuthen: TButton;
    tbAuthenKeyNum: TEdit;
    BinaryGroup: TGroupBox;
    Label6: TLabel;
    tbBinaryBlockNum: TEdit;
    tbLen: TEdit;
    Label7: TLabel;
    Label8: TLabel;
    tbData: TEdit;
    tbRead: TButton;
    tbUpdate: TButton;
    ValueGroup: TGroupBox;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    tbValueBlockNum: TEdit;
    tbSource: TEdit;
    tbTarget: TEdit;
    btnStore: TButton;
    btnInc: TButton;
    btnDec: TButton;
    btnReadValue: TButton;
    btnRestore: TButton;
    tbValue: TEdit;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure tbKeyNumKeyPress(Sender: TObject; var Key: Char);
    procedure tbKeyVal1KeyPress(Sender: TObject; var Key: Char);
    procedure tbKeyVal2KeyPress(Sender: TObject; var Key: Char);
    procedure tbKeyVal3KeyPress(Sender: TObject; var Key: Char);
    procedure tbKeyVal4KeyPress(Sender: TObject; var Key: Char);
    procedure tbKeyVal5KeyPress(Sender: TObject; var Key: Char);
    procedure tbKeyVal6KeyPress(Sender: TObject; var Key: Char);
    procedure btnLoadClick(Sender: TObject);
    procedure btnAuthenClick(Sender: TObject);
    procedure tbReadClick(Sender: TObject);
    procedure tbUpdateClick(Sender: TObject);
    procedure btnStoreClick(Sender: TObject);
    procedure btnReadValueClick(Sender: TObject);
    procedure btnIncClick(Sender: TObject);
    procedure btnDecClick(Sender: TObject);
    procedure btnRestoreClick(Sender: TObject);
    procedure btnQuitClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmMifare: TfrmMifare;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  ATRVal              : array [0..128] of byte;
  ATRLen              : ^DWORD;

implementation

{$R *.dfm}

procedure DisplayOut(output: String; mode: integer);
begin

  case mode of
    1: frmMifare.rbOutput.SelAttributes.Color := clBlue;
    2: frmMifare.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmMifare.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmMifare.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmMifare.rbOutput.Lines.Add(output);
  frmMifare.rbOutput.SelAttributes.Color := clBlack;
  frmMifare.rbOutput.SetFocus;

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

  frmMifare.btnInit.Enabled := true;
  frmMifare.btnConnect.Enabled := false;
  frmMifare.LoadGroup.Enabled := false;
  frmMifare.AuthenGroup.Enabled := false;
  frmMifare.BinaryGroup.Enabled := false;
  frmMifare.ValueGroup.Enabled := false;
  frmMifare.rKeyA.Checked := true;
  frmMifare.cbReader.Clear;
  frmMifare.tbKeyNum.Text := '';
  frmMifare.tbKeyVal1.Text := '';
  frmMifare.tbKeyVal2.Text := '';
  frmMifare.tbKeyVal3.Text := '';
  frmMifare.tbKeyVal4.Text := '';
  frmMifare.tbKeyVal5.Text := '';
  frmMifare.tbKeyVal6.Text := '';
  frmMifare.tbBlockNum.Text := '';
  frmMifare.tbAuthenKeyNum.Text := '';
  frmMifare.tbBinaryBlockNum.Text := '';
  frmMifare.tbAuthenKeyNum.Text := '';
  frmMifare.tbData.Text := '';
  frmMifare.tbValue.Text := '';
  frmMifare.tbValueBlockNum.Text := '';
  frmMifare.tbSource.Text := '';
  frmMifare.tbTarget.Text := '';
  frmMifare.rbOutput.Clear;
  DisplayOut('Program ready', 1);
  
end;

procedure TfrmMifare.btnInitClick(Sender: TObject);
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

procedure TfrmMifare.btnConnectClick(Sender: TObject);
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

  frmMifare.btnConnect.Enabled := false;
  frmMifare.LoadGroup.Enabled := true;
  frmMifare.AuthenGroup.Enabled := true;
  frmMifare.BinaryGroup.Enabled := true;
  frmMifare.ValueGroup.Enabled := true;

end;

procedure TfrmMifare.tbKeyNumKeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Not (Key in ['0'..'9'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.tbKeyVal1KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.tbKeyVal2KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.tbKeyVal3KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.tbKeyVal4KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.tbKeyVal5KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.tbKeyVal6KeyPress(Sender: TObject; var Key: Char);
begin
  if Key <> chr($08) then begin
    if Key in ['a'..'z'] then
      Dec(Key, 32);
    if Not (Key in ['0'..'9', 'A'..'F'])then
      Key := Chr($00);
  end;
end;

procedure TfrmMifare.btnLoadClick(Sender: TObject);
begin

  if tbKeyNum.Text = '' then begin
    tbKeyNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbKeyNum.Text) > 1 then begin
    tbKeyNum.Text := '1';
    tbKeyNum.SetFocus;
    Exit;
  end;

  if tbKeyVal1.Text = '' then begin
    tbKeyVal1.SetFocus;
    Exit;
  end;
  if tbKeyVal2.Text = '' then begin
    tbKeyVal2.SetFocus;
    Exit;
  end;
  if tbKeyVal3.Text = '' then begin
    tbKeyVal3.SetFocus;
    Exit;
  end;
  if tbKeyVal4.Text = '' then begin
    tbKeyVal4.SetFocus;
    Exit;
  end;
  if tbKeyVal5.Text = '' then begin
    tbKeyVal5.SetFocus;
    Exit;
  end;
  if tbKeyVal6.Text = '' then begin
    tbKeyVal6.SetFocus;
    Exit;
  end;

  ClearBuffers();
  //Load Authentication Keys command
  SendBuff[0] := $FF;                             //Class
  SendBuff[1] := $82;                             //INS
  SendBuff[2] := $00;                             //P1 : Key Structure
  SendBuff[3] := StrToInt(tbKeyNum.Text);         //P2 : Key Number
  SendBuff[4] := $06;                             //P3 : Lc
  SendBuff[5] := StrToInt('$' + tbKeyVal1.Text);  //Key 1
  SendBuff[6] := StrToInt('$' + tbKeyVal2.Text);  //Key 2
  SendBuff[7] := StrToInt('$' + tbKeyVal3.Text);  //Key 3
  SendBuff[8] := StrToInt('$' + tbKeyVal4.Text);  //Key 4
  SendBuff[9] := StrToInt('$' + tbKeyVal5.Text);  //Key 5
  SendBuff[10] := StrToInt('$' + tbKeyVal6.Text); //Key 6

  SendLen := 11;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmMifare.btnAuthenClick(Sender: TObject);
begin

  if tbBlockNum.Text = '' then begin
    tbBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbBlockNum.Text) > 319 then begin
    tbBlockNum.Text := '319';
    tbBlockNum.SetFocus;
    Exit;
  end;

  if tbAuthenKeyNum.Text = '' then begin
    tbAuthenKeyNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbAuthenKeyNum.Text) > 1 then begin
    tbAuthenKeyNum.Text := '1';
    tbAuthenKeyNum.SetFocus;
    Exit;
  end;

  ClearBuffers();
  //Authentication command
  SendBuff[0] := $FF;                       //Class
  SendBuff[1] := $86;                       //INS
  SendBuff[2] := $00;                       //P1
  SendBuff[3] := $00;                       //P2
  SendBuff[4] := $05;                       //Lc
  SendBuff[5] := $01;                       //Byte 1 : Version Number
  SendBuff[6] := $00;                       //Byte 2
  SendBuff[7] := StrToInt(tbBlockNum.Text); //Byte 3 : Block Number

  if rKeyA.Checked then begin
    SendBuff[8] := $60;                     //Byte 4 : Key Type A
  end
  else begin
    SendBuff[8] := $61;                     //Byte 4 : Key Type B
  end;

  SendBuff[9] := StrToInt(tbAuthenKeyNum.Text); //Byte 5 : Key Number

  SendLen := 10;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmMifare.tbReadClick(Sender: TObject);
var index: integer; tempstr: string;
begin

  if tbBinaryBlockNum.Text = '' then begin
    tbBinaryBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbBinaryBlockNum.Text) > 319 then begin
    tbBinaryBlockNum.Text := '319';
    tbBinaryBlockNum.SetFocus;
    Exit;
  end;

  if tbLen.Text = '' then begin
    tbLen.SetFocus;
    Exit;
  end;

  if StrToInt(tbLen.Text) > 16 then begin
    tbLen.Text := '16';
    tbLen.SetFocus;
    Exit;
  end;

  ClearBuffers();
  //Read Binary Block command
  SendBuff[0] := $FF;                               //Class
  SendBuff[1] := $B0;                               //INS
  SendBuff[2] := $00;                               //P1
  SendBuff[3] := StrToInt(tbBinaryBlockNum.Text);   //P2 : Block Number
  SendBuff[4] := StrToInt(tbLen.Text);              //Le : Number of Bytes to read

  SendLen := 5;
  RecvLen := StrToInt(tbLen.Text) + 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end
  else begin
    for index := RecvLen - 2 to RecvLen - 1 do
      tempstr := tempstr + Format('%.02X', [RecvBuff[index]]);

    if tempstr = '9000' then begin
      tempstr := '';
      for index := 0 to RecvLen - 3 do
        tempstr := tempstr + chr(RecvBuff[index]);
      tbData.Text := tempstr;
    end;
  end;

end;

procedure TfrmMifare.tbUpdateClick(Sender: TObject);
var index: integer; tempstr: string;
begin

  if tbData.Text = '' then begin
    tbData.SetFocus;
    Exit;
  end;

  if tbBinaryBlockNum.Text = '' then begin
    tbBinaryBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbBinaryBlockNum.Text) > 319 then begin
    tbBinaryBlockNum.Text := '319';
    tbBinaryBlockNum.SetFocus;
    Exit;
  end;

  if tbLen.Text = '' then begin
    tbLen.SetFocus;
    Exit;
  end;

  if StrToInt(tbLen.Text) > 16 then begin
    tbLen.Text := '16';
    tbLen.SetFocus;
    Exit;
  end;

  if StrToInt(tbLen.Text) > 0 then begin
    if tbData.Text = '' then begin
      tbData.SetFocus;
      Exit;
    end;
  end;

  tempstr := tbData.Text;
  ClearBuffers();
  //Update Binary Block command
  SendBuff[0] := $FF;
  SendBuff[1] := $D6;
  SendBuff[2] := $00;
  SendBuff[3] := StrToInt(tbBinaryBlockNum.Text);
  SendBuff[4] := StrToInt(tbLen.Text);

  for index := 0 to Length(tbData.Text) - 1 do
    SendBuff[index+5] := ord(tempstr[index+1]);

  SendLen := SendBuff[4] + 5;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmMifare.btnStoreClick(Sender: TObject);
var amount: DWORD;
begin

  if tbValue.Text = '' then begin
    tbValue.SetFocus;
    Exit;
  end;

  if StrToInt64(tbValue.Text) > 4294967295 then begin
    tbValue.Text := '4294967295';
    tbValue.SetFocus;
    Exit;
  end;

  if tbValueBlockNum.Text = '' then begin
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbValueBlockNum.Text) > 319 then begin
    tbValueBlockNum.Text := '319';
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  tbSource.Text := '';
  tbTarget.Text := '';

  amount := StrToInt64(tbValue.Text);
  ClearBuffers();
  //Store Value command
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tbValueBlockNum.Text);  // P2 : Block No.
  SendBuff[4] := $05;                             // Lc : Data length
  SendBuff[5] := $00;                             // VB_OP Value
	SendBuff[6] := (Amount shr 24) and $FF;	        // Amount MSByte
	SendBuff[7] := (Amount shr 16) and $FF;	        // Amount middle byte
	SendBuff[8] := (Amount shr 8) and $FF;	        // Amount middle byte
	SendBuff[9] := Amount and $FF;			            // Amount LSByte

  SendLen := 10;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmMifare.btnReadValueClick(Sender: TObject);
var amount: DWORD;
begin


  if tbValueBlockNum.Text = '' then begin
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbValueBlockNum.Text) > 319 then begin
    tbValueBlockNum.Text := '319';
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  tbValue.Text := '';
  tbSource.Text := '';
  tbTarget.Text := '';

  ClearBuffers();
  //Read Value Block command
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $B1;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tbValueBlockNum.Text);  // P2 : Block No.
  SendBuff[4] := $04;                             // Le

  SendLen := 5;
  RecvLen := 6;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

	Amount := RecvBuff[3];
	Amount := Amount + (RecvBuff[2] * 256);
	Amount := Amount + (RecvBuff[1] * 256 * 256);
	Amount := Amount + (RecvBuff[0] * 256 * 256 * 256);
  tbValue.Text := IntToStr(Amount);

end;

procedure TfrmMifare.btnIncClick(Sender: TObject);
var amount: DWORD;
begin


  if tbValue.Text = '' then begin
    tbValue.SetFocus;
    Exit;
  end;

  if StrToInt64(tbValue.Text) > 4294967295 then begin
    tbValue.Text := '4294967295';
    tbValue.SetFocus;
    Exit;
  end;

  if tbValueBlockNum.Text = '' then begin
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbValueBlockNum.Text) > 319 then begin
    tbValueBlockNum.Text := '319';
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  tbSource.Text := '';
  tbTarget.Text := '';

  amount := StrToInt64(tbValue.Text);
  ClearBuffers();
  //Increment command
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tbValueBlockNum.Text);  // P2 : Block No.
  SendBuff[4] := $05;                             // Lc : Data length
  SendBuff[5] := $01;                             // VB_OP Value
	SendBuff[6] := (Amount shr 24) and $FF;	        // Amount MSByte
	SendBuff[7] := (Amount shr 16) and $FF;	        // Amount middle byte
	SendBuff[8] := (Amount shr 8) and $FF;	        // Amount middle byte
	SendBuff[9] := Amount and $FF;			            // Amount LSByte

  SendLen := 10;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmMifare.btnDecClick(Sender: TObject);
var amount: DWORD;
begin

  if tbValue.Text = '' then begin
    tbValue.SetFocus;
    Exit;
  end;

  if StrToInt64(tbValue.Text) > 4294967295 then begin
    tbValue.Text := '4294967295';
    tbValue.SetFocus;
    Exit;
  end;

  if tbValueBlockNum.Text = '' then begin
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  if StrToInt(tbValueBlockNum.Text) > 319 then begin
    tbValueBlockNum.Text := '319';
    tbValueBlockNum.SetFocus;
    Exit;
  end;

  tbSource.Text := '';
  tbTarget.Text := '';

  amount := StrToInt64(tbValue.Text);
  ClearBuffers();
  //Increment command
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tbValueBlockNum.Text);  // P2 : Block No.
  SendBuff[4] := $05;                             // Lc : Data length
  SendBuff[5] := $02;                             // VB_OP Value
	SendBuff[6] := (Amount shr 24) and $FF;	        // Amount MSByte
	SendBuff[7] := (Amount shr 16) and $FF;	        // Amount middle byte
	SendBuff[8] := (Amount shr 8) and $FF;	        // Amount middle byte
	SendBuff[9] := Amount and $FF;			            // Amount LSByte

  SendLen := 10;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmMifare.btnRestoreClick(Sender: TObject);
begin

  if tbSource.Text = '' then begin
    tbSource.SetFocus;
    Exit;
  end;

  if StrToInt(tbSource.Text) > 319 then begin
    tbSource.Text := '319';
    tbSource.SetFocus;
    Exit;
  end;

  if tbTarget.Text = '' then begin
    tbTarget.SetFocus;
    Exit;
  end;

  if StrToInt(tbTarget.Text) > 319 then begin
    tbTarget.Text := '319';
    tbTarget.SetFocus;
    Exit;
  end;

  tbValue.Text := '';
  tbValueBlockNum.Text := '';

  ClearBuffers();
  //Restore Value command
  SendBuff[0] := $FF;                             // CLA
  SendBuff[1] := $D7;                             // INS
  SendBuff[2] := $00;                             // P1
  SendBuff[3] := StrToInt(tbSource.Text);         // P2 : Source Block No.
  SendBuff[4] := $02;                             // Lc
  SendBuff[5] := $03;                             // Data In Byte 1
  SendBuff[6] := StrToInt(tbTarget.Text);         // P2 : Target Block No.

  SendLen := 7;
  RecvLen := 2;

  retCode := SendAPDU();
  if retCode <> SCARD_S_SUCCESS then
    Exit;

end;

procedure TfrmMifare.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;
  
end;

procedure TfrmMifare.btnClearClick(Sender: TObject);
begin

  rbOutput.Clear;

end;

procedure TfrmMifare.btnResetClick(Sender: TObject);
begin

  retCode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := ScardReleaseContext(hContext);
  Initialize();
  
end;

procedure TfrmMifare.FormActivate(Sender: TObject);
begin

  Initialize();

end;

end.
