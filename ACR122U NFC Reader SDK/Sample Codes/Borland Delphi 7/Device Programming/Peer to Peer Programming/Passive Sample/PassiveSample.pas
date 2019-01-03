//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              frmPassive.pas
//
//  Description:       This sample program outlines the steps
//                     on how to set an ACR122 NFC reader to its
//                     passive mode and receive data
//
//  Author:            Wazer Emmanuel R. Benal
//
//  Date:              August 5, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

unit PassiveSample;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, AcsModule, ComCtrls;

const MAX_BUFFER_LEN    = 256;
const IOCTL_CCID_ESCAPE_SCARD_CTL_CODE = 3211264 + 3500 * 4;

type
  TfrmPassive = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    btnInit: TButton;
    btnConnect: TButton;
    btnPassive: TButton;
    rbOutput: TRichEdit;
    RecvGroup: TGroupBox;
    tbData: TMemo;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnPassiveClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnQuitClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmPassive: TfrmPassive;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  data        : string;

implementation

{$R *.dfm}

procedure DisplayOut(output: String; mode: integer);
begin

  case mode of
    1: frmPassive.rbOutput.SelAttributes.Color := clBlue;
    2: frmPassive.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmPassive.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmPassive.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmPassive.rbOutput.Lines.Add(output);
  frmPassive.rbOutput.SelAttributes.Color := clBlack;
  frmPassive.rbOutput.SetFocus;

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

function CardControl(): integer;
var index: integer; tempstr: String;
begin


  tempstr := '';
  for index := 0 to SendLen - 1 do
    tempstr := tempstr + Format('%.02X ', [SendBuff[index]]);

  DisplayOut(tempstr,3);

  retCode := SCardControl(hCard,
                          IOCTL_CCID_ESCAPE_SCARD_CTL_CODE,
                          @SendBuff,
                          SendLen,
                          @RecvBuff,
                          RecvLen,
                          @nBytesRet);

  if retcode <> SCARD_S_SUCCESS then begin
    DisplayOut(GetScardErrMsg(retcode),2);
    CardControl := retcode;
    Exit;
  end;

  tempstr := '';
  for index := 0 to RecvLen - 1 do
    tempstr := tempstr + Format('%.02X ', [RecvBuff[index]]);

  DisplayOut(tempstr, 4);

  CardControl := retcode;

end;

procedure Initialize();
begin

  frmPassive.btnInit.Enabled := true;
  frmPassive.btnConnect.Enabled := false;
  frmPassive.btnPassive.Enabled := false;
  frmPassive.RecvGroup.Enabled := false;
  frmPassive.cbReader.Text := '';
  frmPassive.rbOutput.Text := '';
  frmPassive.tbData.Text := '';
  DisplayOut('Program ready',1);

end;

procedure RecvData();
var datalen: Byte; index: integer;
begin

  data := '';
  //Receive first the length
  //of the actual data to be
  //received
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $02;
  SendBuff[5] := $D4;
  SendBuff[6] := $86;

  SendLen := 7;
  RecvLen := 6;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  datalen := RecvBuff[3];

  //Send a response with a value of 90 00
  //to the sending device
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $04;
  SendBuff[5] := $D4;
  SendBuff[6] := $8E;
  SendBuff[7] := $90;
  SendBuff[8] := $00;

  SendLen := 9;
  RecvLen := 5;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  //Receive the actual data
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $02;
  SendBuff[5] := $D4;
  SendBuff[6] := $86;

  SendLen := 7;
  RecvLen := datalen + 5;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  for index := 3 to recvlen - 3 do
    data := data + chr(RecvBuff[index]);

  //Send a response with a value of 90 00
  //to the sending device
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $04;
  SendBuff[5] := $D4;
  SendBuff[6] := $8E;
  SendBuff[7] := $90;
  SendBuff[8] := $00;

  SendLen := 9;
  RecvLen := 5;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  frmPassive.tbData.Text := data;

end;

procedure SetPassive();
var index: integer; tempstr: string;
begin

  //Setup passive mode
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $27;
  SendBuff[5] := $D4;
  SendBuff[6] := $8C;
  SendBuff[7] := $00;
  SendBuff[8] := $08;
  SendBuff[9] := $00;
  SendBuff[10] := $12;
  SendBuff[11] := $34;
  SendBuff[12] := $56;
  SendBuff[13] := $40;
  SendBuff[14] := $01;
  SendBuff[15] := $FE;
  SendBuff[16] := $A2;
  SendBuff[17] := $A3;
  SendBuff[18] := $A4;
  SendBuff[19] := $A5;
  SendBuff[20] := $A6;
  SendBuff[21] := $A7;
  SendBuff[22] := $C0;
  SendBuff[23] := $C1;
  SendBuff[24] := $C2;
  SendBuff[25] := $C3;
  SendBuff[26] := $C4;
  SendBuff[27] := $C5;
  SendBuff[28] := $C6;
  SendBuff[29] := $C7;
  SendBuff[30] := $FF;
  SendBuff[31] := $FF;
  SendBuff[32] := $AA;
  SendBuff[33] := $99;
  SendBuff[34] := $88;
  SendBuff[35] := $77;
  SendBuff[36] := $66;
  SendBuff[37] := $55;
  SendBuff[38] := $44;
  SendBuff[39] := $33;
  SendBuff[40] := $22;
  SendBuff[41] := $11;
  SendBuff[42] := $00;
  SendBuff[43] := $00;

  SendLen := 44;
  RecvLen := 22;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  for index := RecvLen - 2 to RecvLen - 1 do
    tempstr := tempstr + Format('%.02X', [RecvBuff[index]]);

  if tempstr <> '9000' then begin
    DisplayOut('Set passive failed', 2);
    Exit;
  end;

  RecvData();

end;

procedure GetFirmWare();
var tempstr: string; index: integer;
begin

  //Get firmware of the reader
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $48;
  SendBuff[3] := $00;
  SendBuff[4] := $00;

  SendLen := 5;
  RecvLen := 10;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  tempstr := '';
  for index := 0 to RecvLen - 1 do
    tempstr := tempstr + chr(RecvBuff[index]);

  DisplayOut('Firmware version: ' + tempstr, 1);

end;

procedure TfrmPassive.btnInitClick(Sender: TObject);
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

procedure TfrmPassive.btnConnectClick(Sender: TObject);
begin

  //Connect to reader using a shared connection
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);

  if retcode <> SCARD_S_SUCCESS then begin
    //Connect to reader using a direct connection
    retCode := SCardConnectA(hContext,
                             PChar(cbReader.Text),
                             SCARD_SHARE_DIRECT,
                             0,
                             @hCard,
                             @dwActProtocol);
    if retcode <> SCARD_S_SUCCESS then begin
      displayout(GetScardErrMsg(retcode),2);
    end
    else begin
      displayout('Successful connection to ' + cbReader.Text, 1)
    end;
  end
  else begin
    displayout('Successful connection to ' + cbReader.Text, 1)
  end;

  GetFirmWare();

  frmPassive.btnConnect.Enabled := false;
  frmPassive.btnPassive.Enabled := true;
  frmPassive.RecvGroup.Enabled := true;

end;

procedure TfrmPassive.btnPassiveClick(Sender: TObject);
begin

  SetPassive();

end;

procedure TfrmPassive.btnClearClick(Sender: TObject);
begin

  rbOutput.Clear;
  
end;

procedure TfrmPassive.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;

end;

procedure TfrmPassive.FormActivate(Sender: TObject);
begin

  Initialize();
  
end;

procedure TfrmPassive.btnResetClick(Sender: TObject);
begin

  retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retcode := ScardReleaseContext(hContext);
  Initialize();
  
end;

end.
