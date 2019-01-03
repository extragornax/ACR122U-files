//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              GetATR.pas
//
//  Description:       This sample program demonstrates how to
//                     get the ATR using ACR122
//
//  Author:	           Wazer Emmanuel R. Benal
//
//  Date:	             July 30, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

unit GetATR;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, AcsModule;

const MAX_BUFFER_LEN    = 256;

type
  TfrmGetATR = class(TForm)
    Label1: TLabel;
    cbReader: TComboBox;
    btnInit: TButton;
    btnConnect: TButton;
    btnATR: TButton;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    rbOutput: TRichEdit;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnQuitClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnATRClick(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmGetATR: TfrmGetATR;
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
    1: frmGetATR.rbOutput.SelAttributes.Color := clBlue;
    2: frmGetATR.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmGetATR.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmGetATR.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmGetATR.rbOutput.Lines.Add(output);
  frmGetATR.rbOutput.SelAttributes.Color := clBlack;
  frmGetATR.rbOutput.SetFocus;

end;

procedure InterpretATR();
var RIDVal, cardName: String;
    indx: Integer;
begin

  // 4. Interpret ATR and guess card
  // 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
  if integer(ATRLen) > 14 then begin
    RIDVal := '';
    for indx := 7 to 11 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = 'A000000306') then begin
      cardName := '';
      case ATRVal[12] of
        0: cardName := 'No card information';
        1: cardName := 'ISO 14443 A, Part1 Card Type';
        2: cardName := 'ISO 14443 A, Part2 Card Type';
        3: cardName := 'ISO 14443 A, Part3 Card Type';
        5: cardName := 'ISO 14443 B, Part1 Card Type';
        6: cardName := 'ISO 14443 B, Part2 Card Type';
        7: cardName := 'ISO 14443 B, Part3 Card Type';
        9: cardName := 'ISO 15693, Part1 Card Type';
        10: cardName := 'ISO 15693, Part2 Card Type';
        11: cardName := 'ISO 15693, Part3 Card Type';
        12: cardName := 'ISO 15693, Part4 Card Type';
        13: cardName := 'Contact Card (7816-10) IIC Card Type';
        14: cardName := 'Contact Card (7816-10) Extended IIC Card Type';
        15: cardName := 'Contact Card (7816-10) 2WBP Card Type';
        16: cardName := 'Contact Card (7816-10) 3WBP Card Type';
      else
        cardName := 'Undefined card';
      end;
      if (ATRVal[12] = $03) then
        if ATRVal[13] = $00 then
          case ATRVal[14] of
            $01: cardName := cardName + ': Mifare Standard 1K';
            $02: cardName := cardName + ': Mifare Standard 4K';
            $03: cardName := cardName + ': Mifare Ultra light';
            $04: cardName := cardName + ': SLE55R_XXXX';
            $06: cardName := cardName + ': SR176';
            $07: cardName := cardName + ': SRI X4K';
            $08: cardName := cardName + ': AT88RF020';
            $09: cardName := cardName + ': AT88SC0204CRF';
            $0A: cardName := cardName + ': AT88SC0808CRF';
            $0B: cardName := cardName + ': AT88SC1616CRF';
            $0C: cardName := cardName + ': AT88SC3216CRF';
            $0D: cardName := cardName + ': AT88SC6416CRF';
            $0E: cardName := cardName + ': SRF55V10P';
            $0F: cardName := cardName + ': SRF55V02P';
            $10: cardName := cardName + ': SRF55V10S';
            $11: cardName := cardName + ': SRF55V02S';
            $12: cardName := cardName + ': TAG IT';
            $13: cardName := cardName + ': LRI512';
            $14: cardName := cardName + ': ICODESLI';
            $15: cardName := cardName + ': TEMPSENS';
            $16: cardName := cardName + ': I.CODE1';
            $17: cardName := cardName + ': PicoPass 2K';
            $18: cardName := cardName + ': PicoPass 2KS';
            $19: cardName := cardName + ': PicoPass 16K';
            $1A: cardName := cardName + ': PicoPass 16KS';
            $1B: cardName := cardName + ': PicoPass 16K(8x2)';
            $1C: cardName := cardName + ': PicoPass 16KS(8x2)';

            $1D: cardName := cardName + ': PicoPass 32KS(16+16)';
            $1E: cardName := cardName + ': PicoPass 32KS(16+8x2)';
            $1F: cardName := cardName + ': PicoPass 32KS(8x2+16)';
            $20: cardName := cardName + ': PicoPass 32KS(8x2+8x2)';
            $21: cardName := cardName + ': LRI64';
            $22: cardName := cardName + ': I.CODE UID';
            $23: cardName := cardName + ': I.CODE EPC';
            $24: cardName := cardName + ': LRI12';
            $25: cardName := cardName + ': LRI128';
            $26: cardName := cardName + ': Mifare Mini';
          end
        else
          if ATRVal[13] = $FF then
            case ATRVal[14] of
              $09: cardName := cardName + ': Mifare Mini';
            end;
      DisplayOut(cardName + ' is detected', 4);
    end;
   end;

  // 4.2. Mifare DESFire card using ISO 14443 Part 4
  if integer(ATRLen) = 11 then begin
    RIDVal := '';
    for indx := 4 to 9 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '067577810280') then
      DisplayOut('Mifare DESFire is detected', 4);
  end;

  // 4.3. Other cards using ISO 14443 Part 4
  if integer(ATRLen) = 17 then begin
    RIDVal := '';
    for indx := 4 to 15 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '50122345561253544E3381C3') then
      DisplayOut('ST19XRC8E is detected', 4);
  end;

end;

procedure Initialize();
begin

  frmGetATR.rbOutput.Clear;
  frmGetATR.cbReader.Clear;
  frmGetATR.btnInit.Enabled := true;
  frmGetATR.btnConnect.Enabled := false;
  frmGetATR.btnATR.Enabled := false;
  DisplayOut('Program ready', 1);

end;

procedure TfrmGetATR.btnInitClick(Sender: TObject);
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

procedure TfrmGetATR.btnConnectClick(Sender: TObject);
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

  btnConnect.Enabled := false;
  btnATR.Enabled := true;

end;

procedure TfrmGetATR.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;
  
end;

procedure TfrmGetATR.btnClearClick(Sender: TObject);
begin

  rbOutput.Clear;
  
end;

procedure TfrmGetATR.btnATRClick(Sender: TObject);
var ReaderLen, dwState: ^DWORD;
    tmpStr: String;
    indx: Integer;
    tmpWord: DWORD;
begin

  DisplayOut('Invoke SCardStatus',1);
  // 1. Invoke SCardStatus using hCard handle
  //    and valid reader name
  tmpWord := 32;
  ATRLen := @tmpWord;
  retCode := SCardStatusA(hCard,
                         PChar(cbReader.Text),
                         @ReaderLen,
                         @dwState,
                         @dwActProtocol,
                         @ATRVal,
                         @ATRLen);
  if retCode <> SCARD_S_SUCCESS then begin
    DisplayOut(GetScardErrMsg(retCode), 2);
  end
  else begin
    // 2. Format ATRVal returned and display string as ATR value
    tmpStr := 'ATR Length: ' + InttoStr(integer(ATRLen));
    DisplayOut(tmpStr,4);
    tmpStr := 'ATR Value: ';
    for indx := 0 to integer(ATRLen)-1 do
      tmpStr := tmpStr + Format('%.02X ',[ATRVal[indx]]);
    DisplayOut(tmpStr,4);

    // 3. Interpret dwActProtocol returned and display as active protocol
    tmpStr := 'Active Protocol: ';
    case integer(dwActProtocol) of
      1: tmpStr := tmpStr + 'T=0';
      2: tmpStr := tmpStr + 'T=1';
    else
      tmpStr := 'No protocol is defined.';
    end;
    DisplayOut(tmpStr,4);
  end;

  InterpretATR();

end;

procedure TfrmGetATR.btnResetClick(Sender: TObject);
begin

  retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retcode := ScardReleaseContext(hContext);
  Initialize();

end;

procedure TfrmGetATR.FormActivate(Sender: TObject);
begin

  Initialize();

end;

end.
