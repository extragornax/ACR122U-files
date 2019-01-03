//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              frmActive.pas
//
//  Description:       This sample program outlines the steps
//                     on how to set an ACR122 NFC reader to its
//                     active mode and sending data
//
//  Author:            Wazer Emmanuel R. Benal
//
//  Date:              August 5, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

unit ActiveSample;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, AcsModule;

const MAX_BUFFER_LEN    = 256;
const IOCTL_CCID_ESCAPE_SCARD_CTL_CODE = 3211264 + 3500 * 4;

type
  TfrmActive = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    rbOutput: TRichEdit;
    btnInit: TButton;
    btnConnect: TButton;
    btnActive: TButton;
    SendGroup: TGroupBox;
    tbData: TMemo;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnActiveClick(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnQuitClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmActive: TfrmActive;
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
    1: frmActive.rbOutput.SelAttributes.Color := clBlue;
    2: frmActive.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmActive.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmActive.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmActive.rbOutput.Lines.Add(output);
  frmActive.rbOutput.SelAttributes.Color := clBlack;
  frmActive.rbOutput.SetFocus;

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

procedure SendData();
var tempdata: array [0..512] of byte; data: string; datalen: integer; index: integer;
begin

  //Transfer string data to byte array
  //and determine its length
  data := frmActive.tbData.Text;
  datalen := Length(data);

  for index := 0 to datalen - 1 do
    tempdata[index] := ord(data[index+1]);

  //Send length of the data first
  //so that the receiving device would
  //know how long the data would be
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $01;
  SendBuff[5] := $D4;
  SendBuff[6] := $40;
  SendBuff[7] := $01;
  SendBuff[8] := datalen;

  SendLen := 9;
  RecvLen := 7;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  //Send actual data
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := datalen;
  SendBuff[5] := $D4;
  SendBuff[6] := $40;
  SendBuff[7] := $01;

  for index := 0 to datalen - 1 do
    SendBuff[index+8] := tempdata[index];

  SendLen := datalen + 8;
  RecvLen := 7;

  retcode := CardControl();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure SetActive();
var index: integer; tempstr: string;
begin

  //Setup active mode
  ClearBuffers();
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $00;
  SendBuff[3] := $00;
  SendBuff[4] := $0A;
  SendBuff[5] := $D4;
  SendBuff[6] := $56;
  SendBuff[7] := $01;
  SendBuff[8] := $02;
  SendBuff[9] := $01;
  SendBuff[10] := $00;
  SendBuff[11] := $FF;
  SendBuff[12] := $FF;
  SendBuff[13] := $00;
  SendBuff[14] := $00;

  SendLen := 15;
  RecvLen := 21;

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

  SendData();
  
end;

procedure Initialize();
begin

  frmActive.btnInit.Enabled := true;
  frmActive.btnConnect.Enabled := false;
  frmActive.btnActive.Enabled := false;
  frmActive.SendGroup.Enabled := false;
  frmActive.tbData.Text := '';
  frmActive.rbOutput.Clear;
  DisplayOut('Program ready', 1);

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

procedure TfrmActive.btnInitClick(Sender: TObject);
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

procedure TfrmActive.btnConnectClick(Sender: TObject);
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

  frmActive.btnConnect.Enabled := false;
  frmActive.btnActive.Enabled := true;
  frmActive.SendGroup.Enabled := true;

end;

procedure TfrmActive.btnActiveClick(Sender: TObject);
begin

  SetActive();

end;

procedure TfrmActive.btnResetClick(Sender: TObject);
begin

  retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retcode := ScardReleaseContext(hContext);
  Initialize();
  
end;

procedure TfrmActive.FormActivate(Sender: TObject);
begin

  Initialize();
  
end;

procedure TfrmActive.btnClearClick(Sender: TObject);
begin

  rbOutput.Clear;
  
end;

procedure TfrmActive.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;
  
end;

end.
