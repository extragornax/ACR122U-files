object frmActive: TfrmActive
  Left = 212
  Top = 185
  Width = 630
  Height = 395
  Caption = 'Active Device Sample'
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
    Left = 88
    Top = 8
    Width = 177
    Height = 21
    ItemHeight = 13
    TabOrder = 0
  end
  object rbOutput: TRichEdit
    Left = 272
    Top = 8
    Width = 345
    Height = 345
    Lines.Strings = (
      'RichEdit1')
    TabOrder = 1
  end
  object btnInit: TButton
    Left = 88
    Top = 40
    Width = 177
    Height = 25
    Caption = 'Initialize'
    TabOrder = 2
    OnClick = btnInitClick
  end
  object btnConnect: TButton
    Left = 88
    Top = 72
    Width = 177
    Height = 25
    Caption = 'Connect'
    TabOrder = 3
    OnClick = btnConnectClick
  end
  object btnActive: TButton
    Left = 88
    Top = 104
    Width = 177
    Height = 25
    Caption = 'Set Active Mode and Send Data'
    TabOrder = 4
    OnClick = btnActiveClick
  end
  object SendGroup: TGroupBox
    Left = 8
    Top = 136
    Width = 257
    Height = 121
    Caption = 'Send Data'
    TabOrder = 5
    object tbData: TMemo
      Left = 8
      Top = 16
      Width = 241
      Height = 97
      Lines.Strings = (
        'tbData')
      TabOrder = 0
    end
  end
  object btnClear: TButton
    Left = 144
    Top = 264
    Width = 121
    Height = 25
    Caption = 'Clear Output'
    TabOrder = 6
    OnClick = btnClearClick
  end
  object btnReset: TButton
    Left = 144
    Top = 296
    Width = 121
    Height = 25
    Caption = 'Reset'
    TabOrder = 7
    OnClick = btnResetClick
  end
  object btnQuit: TButton
    Left = 144
    Top = 328
    Width = 121
    Height = 25
    Caption = 'Quit'
    TabOrder = 8
    OnClick = btnQuitClick
  end
end
