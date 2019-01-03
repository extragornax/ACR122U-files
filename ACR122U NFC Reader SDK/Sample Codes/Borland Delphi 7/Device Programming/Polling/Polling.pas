//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              Polling.pas
//
//  Description:       This sample program outlines the steps on how to
//                     execute card detection polling functions using ACR12
//
//  Author:            Wazer Emmanuel R. Benal
//
//  Date:              June 30, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================


unit Polling;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, AcsModule, ExtCtrls;

const MAX_BUFFER_LEN    = 256;

type
  TfrmPoll = class(TForm)
    sbMsg: TStatusBar;
    Label1: TLabel;
    cbReader: TComboBox;
    btnInit: TButton;
    btnConnect: TButton;
    PollingGroup: TGroupBox;
    rbOutput: TRichEdit;
    cbAutoPICC: TCheckBox;
    cbAutoATS: TCheckBox;
    cbTypeA: TCheckBox;
    cbTypeB: TCheckBox;
    cbTopaz: TCheckBox;
    cbFelica212: TCheckBox;
    cbFelica424: TCheckBox;
    IntervalGroup: TGroupBox;
    r250ms: TRadioButton;
    r500ms: TRadioButton;
    btnGetPoll: TButton;
    btnSetPoll: TButton;
    btnStart: TButton;
    btnClear: TButton;
    btnReset: TButton;
    btnQuit: TButton;
    pollTimer: TTimer;
    procedure btnInitClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnQuitClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure btnGetPollClick(Sender: TObject);
    procedure btnSetPollClick(Sender: TObject);
    procedure pollTimerTimer(Sender: TObject);
    procedure btnStartClick(Sender: TObject);
    procedure FormActivate(Sender: TObject);
    procedure btnResetClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmPoll: TfrmPoll;
  hContext            : SCARDCONTEXT;
  hCard               : SCARDCONTEXT;
  ioRequest           : SCARD_IO_REQUEST;
  retCode             : Integer;
  dwActProtocol, BufferLen  : DWORD;
  SendBuff, RecvBuff        : array [0..262] of Byte;
  SendLen, RecvLen, nBytesRet : DWORD;
  Buffer      : array [0..MAX_BUFFER_LEN] of char;
  ATRVal              : array [1..36] of byte;
  ATRLen              : DWORD;
  RdrState    : SCARD_READERSTATE;
  PollStart     : boolean;

implementation

{$R *.dfm}

procedure DisplayOut(output: String; mode: integer);
begin

  case mode of
    1: frmPoll.rbOutput.SelAttributes.Color := clBlue;
    2: frmPoll.rbOutput.SelAttributes.Color := clRed;
    3: begin
          frmPoll.rbOutput.SelAttributes.Color := clBlack;
          output := '<< ' + output;
       end;
    4: begin
          frmPoll.rbOutput.SelAttributes.Color := clBlack;
          output := '>> ' + output;
       end;
  end;

  frmPoll.rbOutput.Lines.Add(output);
  frmPoll.rbOutput.SelAttributes.Color := clBlack;
  frmPoll.rbOutput.SetFocus;

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

procedure InterpretATR();
var RIDVal, cardName: String;
    indx: Integer;
begin

  // 4. Interpret ATR and guess card
  // 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
  if integer(ATRLen) > 14 then begin
    RIDVal := '';
    for indx := 8 to 12 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = 'A000000306') then begin
      cardName := '';
      case ATRVal[13] of
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
      if (ATRVal[13] = $03) then
        if ATRVal[14] = $00 then
          case ATRVal[15] of
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
          if ATRVal[14] = $FF then
            case ATRVal[14] of
              $09: cardName := cardName + ': Mifare Mini';
            end;
      frmPoll.sbMsg.Panels[3].Text := cardname;
      //DisplayOut(cardName + ' is detected', 6);
    end;
   end;

  // 4.2. Mifare DESFire card using ISO 14443 Part 4
  if integer(ATRLen) > 11 then begin
    RIDVal := '';
    for indx := 5 to 10 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '067577810280') then
      frmPoll.sbMsg.Panels[3].Text := 'Mifare DESFire';
      //DisplayOut('Mifare DESFire is detected', 6);
  end;

  // 4.3. Other cards using ISO 14443 Part 4
  if integer(ATRLen) > 17 then begin
    RIDVal := '';
    for indx := 5 to 16 do
      RIDVal := RIDVal + Format('%.02X',[ATRVal[indx]]);
    if (RIDVal = '50122345561253544E3381C3') then
      frmPoll.sbMsg.Panels[3].Text := 'ST19XRC8E';
      //DisplayOut('ST19XRC8E is detected', 6);
  end;

end;

function CheckCard():boolean;
var ReaderLen: ^DWORD;
    dwState: ^DWORD;
    tempword: DWORD;
    index: integer;
begin

  //tempword := 32;
  //ATRLen := @tempword;
  //retCode := SCardStatusA(hCard,
  //                        PChar(frmPoll.cbReader.Text),
  //                        @ReaderLen,
  //                        @State,
  //                        @dwActProtocol,
  //                        @ATRVal,
  //                        @ATRLen);
  RdrState.szReader := PChar(frmPoll.cbReader.Text);
  retCode := SCardGetStatusChangeA(hContext,
                                       0,
                                       @RdrState,
                                       1);


  if retCode = SCARD_S_SUCCESS then begin
    if (RdrState.dwEventStates and SCARD_STATE_PRESENT) <> 0 then begin
      //if RdrState.rgbATR <> 0 then begin
        for index := 1 to 36 do
          ATRVal[index] := RdrState.rgbATR[index];
          ATRLen := Length(RdrState.rgbATR);
        InterpretATR();
        CheckCard := true;
      //end
      //else begin
      //  DisplayOut(GetScardErrMsg(retcode), 2);
      //  CheckCard := false;
      //end;
    end;
  end
  else begin
    CheckCard := false;
  end;

end;

procedure Initialize();
begin

  frmPoll.btnInit.Enabled := true;
  frmPoll.btnConnect.Enabled := false;
  frmPoll.PollingGroup.Enabled := false;
  frmPoll.btnStart.Enabled := false;
  frmPoll.cbReader.Clear;
  frmPoll.cbAutoPICC.Checked := false;
  frmPoll.cbAutoATS.Checked := false;
  frmPoll.cbTypeA.Checked := false;
  frmPoll.cbTypeB.Checked := false;
  frmPoll.cbTopaz.Checked := false;
  frmPoll.cbFelica212.Checked := false;
  frmPoll.cbFelica424.Checked := false;
  frmPoll.rbOutput.Clear;
  DisplayOut('Program ready', 1);

end;

procedure TfrmPoll.btnInitClick(Sender: TObject);
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

procedure TfrmPoll.btnConnectClick(Sender: TObject);
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

  frmPoll.btnConnect.Enabled := false;
  frmPoll.PollingGroup.Enabled := true;
  frmPoll.btnStart.Enabled := true;

end;

procedure TfrmPoll.btnQuitClick(Sender: TObject);
begin

  Application.Terminate;
  
end;

procedure TfrmPoll.btnClearClick(Sender: TObject);
begin

  rbOutput.Clear;

end;

procedure TfrmPoll.btnGetPollClick(Sender: TObject);
//var index: integer; tempstr: string;
begin

  ClearBuffers();
  //Get operating parameters command
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $50;
  SendBuff[3] := $00;
  SendBuff[4] := $00;

  SendLen := 5;
  RecvLen := 2;

  retcode := SendAPDU();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

  if RecvBuff[0] and $80 <> 0 then begin
    DisplayOut('Automatic Polling is enabled', 4);
    cbAutoPICC.Checked := true;
  end
  else begin
    DisplayOut('Automatic Polling is disabled', 4);
    cbAutoPICC.Checked := false;
  end;

  if RecvBuff[0] and $40 <> 0 then begin
    DisplayOut('Automatic ATS Generation is enabled', 4);
    cbAutoATS.Checked := true;
  end
  else begin
    DisplayOut('Automatic ATS Generation is disabled', 4);
    cbAutoATS.Checked := false;
  end;

  if RecvBuff[0] and $20 <> 0 then begin
    DisplayOut('250 ms', 4);
    r250ms.Checked := true;
  end
  else begin
    DisplayOut('500ms', 4);
    r500ms.Checked := true;
  end;

  if RecvBuff[0] and $10 <> 0 then begin
    DisplayOut('Detect Felica 424K Card enabled', 4);
    cbFelica424.Checked := true;
  end
  else begin
    DisplayOut('Detect Felica 424K Card disabled', 4);
    cbFelica424.Checked := false;
  end;

  if RecvBuff[0] and $08 <> 0 then begin
    DisplayOut('Detect Felica 212K Card enabled', 4);
    cbFelica212.Checked := true;
  end
  else begin
    DisplayOut('Detect Felica 212K Card disabled', 4);
    cbFelica212.Checked := false;
  end;

  if RecvBuff[0] and $04 <> 0 then begin
    DisplayOut('Detect Topaz Card enabled', 4);
    cbTopaz.Checked := true;
  end
  else begin
    DisplayOut('DDetect Topaz Card disabled', 4);
    cbTopaz.Checked := false;
  end;

  if RecvBuff[0] and $02 <> 0 then begin
    DisplayOut('Detect ISO14443 Type B Card enabled', 4);
    cbTypeB.Checked := true;
  end
  else begin
    DisplayOut('Detect ISO14443 Type B Card disabled', 4);
    cbTypeB.Checked := false;
  end;

  if RecvBuff[0] and $01 <> 0 then begin
    DisplayOut('Detect ISO14443 Type A Card enabled', 4);
    cbTypeA.Checked := true;
  end
  else begin
    DisplayOut('DDetect ISO14443 Type A Card disabled', 4);
    cbTypeA.Checked := false;
  end;

end;

procedure TfrmPoll.btnSetPollClick(Sender: TObject);
begin

  ClearBuffers();
  //Set operating parameters command
  SendBuff[0] := $FF;
  SendBuff[1] := $00;
  SendBuff[2] := $51;
  SendBuff[3] := $00;

  if cbTypeA.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $01;
    Displayout('Detect ISO14443 Type A Card enabled', 4);
  end
  else begin
    Displayout('Detect ISO14443 Type A Card disabled', 4);
  end;

  if cbTypeB.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $02;
    Displayout('Detect ISO14443 Type B Card enabled', 4);
  end
  else begin
    Displayout('Detect ISO14443 Type B Card disabled', 4);
  end;

  if cbTopaz.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $04;
    Displayout('Detect Topaz Card enabled', 4);
  end
  else begin
    Displayout('Detect Topaz Card disabled', 4);
  end;

  if cbFelica212.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $08;
    Displayout('Detect Felica 212K Card enabled', 4);
  end
  else begin
    Displayout('Detect Felica 212K Card disabled', 4);
  end;

  if cbFelica424.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $10;
    Displayout('Detect Felica 424K Card enabled', 4);
  end
  else begin
    Displayout('Detect Felica 424K Card disabled', 4);
  end;

  if r250ms.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $20;
    pollTimer.Interval := 250;
    DisplayOut('Poll interval is 250 ms', 4);
  end
  else begin
    pollTimer.Interval := 500;
    DisplayOut('Poll interval is 500ms', 4);
  end;

  if cbAutoATS.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $40;
    Displayout('Automatic ATS Generation is enabled', 4);
  end
  else begin
    Displayout('Automatic ATS Generation is enabled', 4);
  end;

  if cbAutoPICC.Checked = true then begin
    SendBuff[3] := SendBuff[3] Or $80;
    Displayout('Automatic Polling is enabled', 4);
  end
  else begin
    Displayout('Automatic Polling is enabled', 4);
  end;

  SendBuff[4] := $00;

  SendLen := 5;
  RecvLen := 2;

  retcode := SendAPDU();
  if retcode <> SCARD_S_SUCCESS then begin
    Exit;
  end;

end;

procedure TfrmPoll.pollTimerTimer(Sender: TObject);
begin

  //retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retCode := SCardConnectA(hContext,
                           PChar(cbReader.Text),
                           SCARD_SHARE_SHARED,
                           SCARD_PROTOCOL_T0 or SCARD_PROTOCOL_T1,
                           @hCard,
                           @dwActProtocol);

  if retcode <> SCARD_S_SUCCESS then begin
    sbMsg.Panels[1].Text := 'No card within range';
    sbMsg.Panels[3].Text := '';
  end
  else begin
    if CheckCard = true then begin
    sbMsg.Panels[1].Text := 'Card is detected';
    //DisplayOut('Card is detected', 5);
    end
    else begin
    sbMsg.Panels[1].Text := 'No card within range';
    sbMsg.Panels[3].Text := '';
    //DisplayOut('No Card within range', 5);
    end;
  end;

end;

procedure TfrmPoll.btnStartClick(Sender: TObject);
begin

  if PollStart = true then begin
    DisplayOut('Polling stopped', 1);
    btnStart.Caption := 'Start Polling';
    sbMsg.Panels[1].Text := '';
    sbMsg.Panels[3].Text := '';
    pollTimer.Enabled := false;
    PollStart := false;
    Exit;
  end;

  btnStart.Caption := 'End Polling';
  DisplayOut('Polling Started', 1);
  pollTimer.Enabled := true;
  PollStart := true;

end;

procedure TfrmPoll.FormActivate(Sender: TObject);
begin

  pollTimer.Enabled := false;
  PollStart := false;
  Initialize();
  
end;

procedure TfrmPoll.btnResetClick(Sender: TObject);
begin

  retcode := ScardDisconnect(hCard, SCARD_UNPOWER_CARD);
  retcode := ScardReleaseContext(hContext);                
  Initialize();
end;

end.
