object frmGetATR: TfrmGetATR
  Left = 290
  Top = 106
  Width = 620
  Height = 350
  Caption = 'Get ATR'
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
    Width = 185
    Height = 21
    ItemHeight = 13
    TabOrder = 0
  end
  object btnInit: TButton
    Left = 152
    Top = 40
    Width = 113
    Height = 25
    Caption = 'Initialize'
    TabOrder = 1
    OnClick = btnInitClick
  end
  object btnConnect: TButton
    Left = 152
    Top = 72
    Width = 113
    Height = 25
    Caption = 'Connect'
    TabOrder = 2
    OnClick = btnConnectClick
  end
  object btnATR: TButton
    Left = 152
    Top = 104
    Width = 113
    Height = 25
    Caption = 'Get ATR'
    TabOrder = 3
    OnClick = btnATRClick
  end
  object btnClear: TButton
    Left = 152
    Top = 216
    Width = 113
    Height = 25
    Caption = 'Clear Output'
    TabOrder = 4
    OnClick = btnClearClick
  end
  object btnReset: TButton
    Left = 152
    Top = 248
    Width = 113
    Height = 25
    Caption = 'Reset'
    TabOrder = 5
    OnClick = btnResetClick
  end
  object btnQuit: TButton
    Left = 152
    Top = 280
    Width = 113
    Height = 25
    Caption = 'Quit'
    TabOrder = 6
    OnClick = btnQuitClick
  end
  object rbOutput: TRichEdit
    Left = 272
    Top = 8
    Width = 329
    Height = 297
    Lines.Strings = (
      'rbOutput')
    TabOrder = 7
  end
end
