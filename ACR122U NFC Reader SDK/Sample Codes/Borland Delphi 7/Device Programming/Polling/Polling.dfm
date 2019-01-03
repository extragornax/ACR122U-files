object frmPoll: TfrmPoll
  Left = 318
  Top = 301
  Width = 593
  Height = 429
  Caption = 'Polling Sample'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 16
    Width = 68
    Height = 13
    Caption = 'Select Reader'
  end
  object sbMsg: TStatusBar
    Left = 0
    Top = 369
    Width = 585
    Height = 26
    Panels = <
      item
        Text = 'Card Status'
        Width = 70
      end
      item
        Width = 150
      end
      item
        Text = 'Card Type'
        Width = 70
      end
      item
        Width = 50
      end>
  end
  object cbReader: TComboBox
    Left = 88
    Top = 8
    Width = 169
    Height = 21
    ItemHeight = 13
    TabOrder = 1
  end
  object btnInit: TButton
    Left = 152
    Top = 40
    Width = 105
    Height = 25
    Caption = 'Initialize'
    TabOrder = 2
    OnClick = btnInitClick
  end
  object btnConnect: TButton
    Left = 152
    Top = 72
    Width = 105
    Height = 25
    Caption = 'Connect'
    TabOrder = 3
    OnClick = btnConnectClick
  end
  object PollingGroup: TGroupBox
    Left = 8
    Top = 104
    Width = 249
    Height = 225
    Caption = 'Polling Options'
    TabOrder = 4
    object cbAutoPICC: TCheckBox
      Left = 16
      Top = 24
      Width = 137
      Height = 17
      Caption = 'Automatic PICC Polling'
      TabOrder = 0
    end
    object cbAutoATS: TCheckBox
      Left = 16
      Top = 40
      Width = 153
      Height = 17
      Caption = 'Automatic ATS Generation'
      TabOrder = 1
    end
    object cbTypeA: TCheckBox
      Left = 16
      Top = 56
      Width = 193
      Height = 17
      Caption = 'Detect ISO14443 Type A Cards'
      TabOrder = 2
    end
    object cbTypeB: TCheckBox
      Left = 16
      Top = 72
      Width = 185
      Height = 17
      Caption = 'Detect ISO14443 Type B Cards'
      TabOrder = 3
    end
    object cbTopaz: TCheckBox
      Left = 16
      Top = 88
      Width = 137
      Height = 17
      Caption = 'Detect Topaz Cards'
      TabOrder = 4
    end
    object cbFelica212: TCheckBox
      Left = 16
      Top = 104
      Width = 161
      Height = 17
      Caption = 'Detect Felica 212K Cards'
      TabOrder = 5
    end
    object cbFelica424: TCheckBox
      Left = 16
      Top = 120
      Width = 177
      Height = 17
      Caption = 'Detect Felica 424K Cards'
      TabOrder = 6
    end
    object IntervalGroup: TGroupBox
      Left = 16
      Top = 144
      Width = 89
      Height = 65
      Caption = 'Polling Interval'
      TabOrder = 7
      object r250ms: TRadioButton
        Left = 16
        Top = 16
        Width = 57
        Height = 17
        Caption = '250 ms'
        TabOrder = 0
      end
      object r500ms: TRadioButton
        Left = 16
        Top = 40
        Width = 57
        Height = 17
        Caption = '500 ms'
        TabOrder = 1
      end
    end
    object btnGetPoll: TButton
      Left = 128
      Top = 152
      Width = 105
      Height = 25
      Caption = 'Get Polling Options'
      TabOrder = 8
      OnClick = btnGetPollClick
    end
    object btnSetPoll: TButton
      Left = 128
      Top = 184
      Width = 105
      Height = 25
      Caption = 'Set Polling Options'
      TabOrder = 9
      OnClick = btnSetPollClick
    end
  end
  object rbOutput: TRichEdit
    Left = 264
    Top = 8
    Width = 305
    Height = 321
    Lines.Strings = (
      'rbOutput')
    TabOrder = 5
  end
  object btnStart: TButton
    Left = 8
    Top = 336
    Width = 249
    Height = 25
    Caption = 'Start Polling'
    TabOrder = 6
    OnClick = btnStartClick
  end
  object btnClear: TButton
    Left = 264
    Top = 336
    Width = 97
    Height = 25
    Caption = 'Clear Output'
    TabOrder = 7
    OnClick = btnClearClick
  end
  object btnReset: TButton
    Left = 368
    Top = 336
    Width = 97
    Height = 25
    Caption = 'Reset'
    TabOrder = 8
    OnClick = btnResetClick
  end
  object btnQuit: TButton
    Left = 472
    Top = 336
    Width = 97
    Height = 25
    Caption = 'Quit'
    TabOrder = 9
    OnClick = btnQuitClick
  end
  object pollTimer: TTimer
    OnTimer = pollTimerTimer
    Left = 16
    Top = 48
  end
end
