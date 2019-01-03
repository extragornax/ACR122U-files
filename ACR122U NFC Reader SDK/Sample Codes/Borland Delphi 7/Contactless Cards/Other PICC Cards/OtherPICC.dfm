object frmPICC: TfrmPICC
  Left = 250
  Top = 148
  Width = 646
  Height = 408
  Caption = 'Other PICC Card Programming'
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
  object cbReader: TComboBox
    Left = 80
    Top = 8
    Width = 177
    Height = 21
    ItemHeight = 13
    TabOrder = 0
  end
  object btnInit: TButton
    Left = 136
    Top = 40
    Width = 121
    Height = 25
    Caption = 'Initialize'
    TabOrder = 1
    OnClick = btnInitClick
  end
  object btnConnect: TButton
    Left = 136
    Top = 72
    Width = 121
    Height = 25
    Caption = 'Connect'
    TabOrder = 2
    OnClick = btnConnectClick
  end
  object DataGroup: TGroupBox
    Left = 8
    Top = 104
    Width = 249
    Height = 57
    Caption = 'Get Data Function'
    TabOrder = 3
    object check1: TCheckBox
      Left = 8
      Top = 24
      Width = 105
      Height = 17
      Caption = 'ISO 14443 A Card'
      TabOrder = 0
    end
    object btnGetData: TButton
      Left = 128
      Top = 16
      Width = 105
      Height = 25
      Caption = 'Get Data'
      TabOrder = 1
      OnClick = btnGetDataClick
    end
  end
  object SendGroup: TGroupBox
    Left = 8
    Top = 168
    Width = 249
    Height = 201
    Caption = 'Send Card Command'
    TabOrder = 4
    object Label2: TLabel
      Left = 16
      Top = 32
      Width = 178
      Height = 13
      Caption = 'CLA     INS      P1     P2        Lc      Le'
    end
    object Label3: TLabel
      Left = 16
      Top = 80
      Width = 35
      Height = 13
      Caption = 'Data In'
    end
    object tbCLA: TEdit
      Left = 16
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 0
      OnKeyPress = tbCLAKeyPress
    end
    object tbINS: TEdit
      Left = 48
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tbINSKeyPress
    end
    object tbP1: TEdit
      Left = 80
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tbP1KeyPress
    end
    object tbP2: TEdit
      Left = 112
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tbP2KeyPress
    end
    object tbLc: TEdit
      Left = 144
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tbLcKeyPress
    end
    object tbLe: TEdit
      Left = 176
      Top = 48
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tbLeKeyPress
    end
    object tbData: TEdit
      Left = 16
      Top = 96
      Width = 217
      Height = 21
      TabOrder = 6
    end
    object btnSend: TButton
      Left = 112
      Top = 168
      Width = 121
      Height = 25
      Caption = 'Send Card Command'
      TabOrder = 7
      OnClick = btnSendClick
    end
  end
  object rbOutput: TRichEdit
    Left = 264
    Top = 8
    Width = 361
    Height = 313
    Lines.Strings = (
      'rbOutput')
    TabOrder = 5
  end
  object btnClear: TButton
    Left = 264
    Top = 336
    Width = 105
    Height = 25
    Caption = 'Clear Output'
    TabOrder = 6
    OnClick = btnClearClick
  end
  object btnReset: TButton
    Left = 392
    Top = 336
    Width = 105
    Height = 25
    Caption = 'Reset'
    TabOrder = 7
    OnClick = btnResetClick
  end
  object btnQuit: TButton
    Left = 520
    Top = 336
    Width = 105
    Height = 25
    Caption = 'Quit'
    TabOrder = 8
    OnClick = btnQuitClick
  end
end
