object frmPassive: TfrmPassive
  Left = 251
  Top = 263
  Width = 659
  Height = 399
  Caption = 'Passive Device Sample'
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
    Width = 193
    Height = 21
    ItemHeight = 13
    TabOrder = 0
  end
  object btnInit: TButton
    Left = 80
    Top = 40
    Width = 193
    Height = 25
    Caption = 'Initialize'
    TabOrder = 1
    OnClick = btnInitClick
  end
  object btnConnect: TButton
    Left = 80
    Top = 72
    Width = 193
    Height = 25
    Caption = 'Connect'
    TabOrder = 2
    OnClick = btnConnectClick
  end
  object btnPassive: TButton
    Left = 80
    Top = 104
    Width = 193
    Height = 25
    Caption = 'Set Passive Mode and Receive Data'
    TabOrder = 3
    OnClick = btnPassiveClick
  end
  object rbOutput: TRichEdit
    Left = 280
    Top = 8
    Width = 361
    Height = 345
    Lines.Strings = (
      'rbOutput')
    TabOrder = 4
  end
  object RecvGroup: TGroupBox
    Left = 8
    Top = 136
    Width = 265
    Height = 121
    Caption = 'Receive Data'
    TabOrder = 5
    object tbData: TMemo
      Left = 8
      Top = 16
      Width = 249
      Height = 97
      Lines.Strings = (
        'tbData')
      TabOrder = 0
    end
  end
  object btnClear: TButton
    Left = 160
    Top = 264
    Width = 113
    Height = 25
    Caption = 'Clear Output'
    TabOrder = 6
    OnClick = btnClearClick
  end
  object btnReset: TButton
    Left = 160
    Top = 296
    Width = 113
    Height = 25
    Caption = 'Reset'
    TabOrder = 7
    OnClick = btnResetClick
  end
  object btnQuit: TButton
    Left = 160
    Top = 328
    Width = 113
    Height = 25
    Caption = 'Quit'
    TabOrder = 8
    OnClick = btnQuitClick
  end
end
