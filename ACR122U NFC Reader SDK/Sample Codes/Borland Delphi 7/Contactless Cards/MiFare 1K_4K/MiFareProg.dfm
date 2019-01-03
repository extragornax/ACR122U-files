object frmMifare: TfrmMifare
  Left = 250
  Top = 146
  Width = 636
  Height = 564
  Caption = 'MiFare Card Programming'
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
    Width = 185
    Height = 21
    ItemHeight = 13
    TabOrder = 0
  end
  object rbOutput: TRichEdit
    Left = 280
    Top = 192
    Width = 337
    Height = 297
    Lines.Strings = (
      'rbOutput')
    TabOrder = 1
  end
  object btnInit: TButton
    Left = 160
    Top = 40
    Width = 113
    Height = 25
    Caption = 'Initialize'
    TabOrder = 2
    OnClick = btnInitClick
  end
  object btnConnect: TButton
    Left = 160
    Top = 72
    Width = 113
    Height = 25
    Caption = 'Connect'
    TabOrder = 3
    OnClick = btnConnectClick
  end
  object LoadGroup: TGroupBox
    Left = 8
    Top = 104
    Width = 265
    Height = 129
    Caption = 'Load Authentication Keys to Device'
    TabOrder = 4
    object Label2: TLabel
      Left = 24
      Top = 32
      Width = 63
      Height = 13
      Caption = 'Key Store No'
    end
    object Label3: TLabel
      Left = 24
      Top = 64
      Width = 75
      Height = 13
      Caption = 'Key Value Input'
    end
    object tbKeyNum: TEdit
      Left = 104
      Top = 24
      Width = 25
      Height = 21
      MaxLength = 1
      TabOrder = 0
      OnKeyPress = tbKeyNumKeyPress
    end
    object tbKeyVal1: TEdit
      Left = 104
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tbKeyVal1KeyPress
    end
    object tbKeyVal2: TEdit
      Left = 128
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 2
      OnKeyPress = tbKeyVal2KeyPress
    end
    object tbKeyVal3: TEdit
      Left = 152
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 3
      OnKeyPress = tbKeyVal3KeyPress
    end
    object tbKeyVal4: TEdit
      Left = 176
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 4
      OnKeyPress = tbKeyVal4KeyPress
    end
    object tbKeyVal5: TEdit
      Left = 200
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 5
      OnKeyPress = tbKeyVal5KeyPress
    end
    object tbKeyVal6: TEdit
      Left = 224
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 6
      OnKeyPress = tbKeyVal6KeyPress
    end
    object btnLoad: TButton
      Left = 144
      Top = 96
      Width = 105
      Height = 25
      Caption = 'Load Keys'
      TabOrder = 7
      OnClick = btnLoadClick
    end
  end
  object AuthenGroup: TGroupBox
    Left = 8
    Top = 240
    Width = 265
    Height = 137
    Caption = 'Authentication'
    TabOrder = 5
    object Label4: TLabel
      Left = 24
      Top = 40
      Width = 44
      Height = 13
      Caption = 'Block No'
    end
    object Label5: TLabel
      Left = 24
      Top = 72
      Width = 63
      Height = 13
      Caption = 'Key Store No'
    end
    object KeyGroup: TGroupBox
      Left = 152
      Top = 24
      Width = 97
      Height = 65
      Caption = 'Key Type'
      TabOrder = 0
      object rKeyA: TRadioButton
        Left = 16
        Top = 16
        Width = 57
        Height = 17
        Caption = 'Key A'
        TabOrder = 0
      end
      object rKeyB: TRadioButton
        Left = 16
        Top = 40
        Width = 57
        Height = 17
        Caption = 'Key B'
        TabOrder = 1
      end
    end
    object tbBlockNum: TEdit
      Left = 104
      Top = 32
      Width = 25
      Height = 21
      MaxLength = 3
      TabOrder = 1
      OnKeyPress = tbKeyNumKeyPress
    end
    object btnAuthen: TButton
      Left = 144
      Top = 104
      Width = 105
      Height = 25
      Caption = 'Authentication'
      TabOrder = 2
      OnClick = btnAuthenClick
    end
    object tbAuthenKeyNum: TEdit
      Left = 104
      Top = 64
      Width = 25
      Height = 21
      MaxLength = 1
      TabOrder = 3
      OnKeyPress = tbKeyNumKeyPress
    end
  end
  object BinaryGroup: TGroupBox
    Left = 8
    Top = 384
    Width = 265
    Height = 137
    Caption = 'Binary Block Functions'
    TabOrder = 6
    object Label6: TLabel
      Left = 24
      Top = 32
      Width = 44
      Height = 13
      Caption = 'Block No'
    end
    object Label7: TLabel
      Left = 144
      Top = 32
      Width = 33
      Height = 13
      Caption = 'Length'
    end
    object Label8: TLabel
      Left = 24
      Top = 56
      Width = 49
      Height = 13
      Caption = 'Data (text)'
    end
    object tbBinaryBlockNum: TEdit
      Left = 80
      Top = 24
      Width = 25
      Height = 21
      MaxLength = 3
      TabOrder = 0
      OnKeyPress = tbKeyNumKeyPress
    end
    object tbLen: TEdit
      Left = 192
      Top = 24
      Width = 25
      Height = 21
      MaxLength = 2
      TabOrder = 1
      OnKeyPress = tbKeyNumKeyPress
    end
    object tbData: TEdit
      Left = 24
      Top = 72
      Width = 217
      Height = 21
      MaxLength = 16
      TabOrder = 2
    end
    object tbRead: TButton
      Left = 24
      Top = 104
      Width = 105
      Height = 25
      Caption = 'Read Block'
      TabOrder = 3
      OnClick = tbReadClick
    end
    object tbUpdate: TButton
      Left = 136
      Top = 104
      Width = 105
      Height = 25
      Caption = 'Update Block'
      TabOrder = 4
      OnClick = tbUpdateClick
    end
  end
  object ValueGroup: TGroupBox
    Left = 280
    Top = 8
    Width = 337
    Height = 177
    Caption = 'Value Block Functions'
    TabOrder = 7
    object Label9: TLabel
      Left = 24
      Top = 32
      Width = 66
      Height = 13
      Caption = 'Value Amount'
    end
    object Label10: TLabel
      Left = 24
      Top = 64
      Width = 44
      Height = 13
      Caption = 'Block No'
    end
    object Label11: TLabel
      Left = 24
      Top = 96
      Width = 64
      Height = 13
      Caption = 'Source Block'
    end
    object Label12: TLabel
      Left = 24
      Top = 128
      Width = 61
      Height = 13
      Caption = 'Target Block'
    end
    object tbValueBlockNum: TEdit
      Left = 104
      Top = 56
      Width = 25
      Height = 21
      MaxLength = 3
      TabOrder = 0
      OnKeyPress = tbKeyNumKeyPress
    end
    object tbSource: TEdit
      Left = 104
      Top = 88
      Width = 25
      Height = 21
      MaxLength = 3
      TabOrder = 1
      OnKeyPress = tbKeyNumKeyPress
    end
    object tbTarget: TEdit
      Left = 104
      Top = 120
      Width = 25
      Height = 21
      MaxLength = 3
      TabOrder = 2
      OnKeyPress = tbKeyNumKeyPress
    end
    object btnStore: TButton
      Left = 216
      Top = 16
      Width = 105
      Height = 25
      Caption = 'Store Value'
      TabOrder = 3
      OnClick = btnStoreClick
    end
    object btnInc: TButton
      Left = 216
      Top = 48
      Width = 105
      Height = 25
      Caption = 'Increment'
      TabOrder = 4
      OnClick = btnIncClick
    end
    object btnDec: TButton
      Left = 216
      Top = 80
      Width = 105
      Height = 25
      Caption = 'Decrement'
      TabOrder = 5
      OnClick = btnDecClick
    end
    object btnReadValue: TButton
      Left = 216
      Top = 112
      Width = 105
      Height = 25
      Caption = 'Read Value'
      TabOrder = 6
      OnClick = btnReadValueClick
    end
    object btnRestore: TButton
      Left = 216
      Top = 144
      Width = 105
      Height = 25
      Caption = 'Restore Value'
      TabOrder = 7
      OnClick = btnRestoreClick
    end
    object tbValue: TEdit
      Left = 104
      Top = 24
      Width = 97
      Height = 21
      TabOrder = 8
      OnKeyPress = tbKeyNumKeyPress
    end
  end
  object btnClear: TButton
    Left = 280
    Top = 496
    Width = 97
    Height = 25
    Caption = 'Clear Output'
    TabOrder = 8
    OnClick = btnClearClick
  end
  object btnReset: TButton
    Left = 400
    Top = 496
    Width = 97
    Height = 25
    Caption = 'Reset'
    TabOrder = 9
    OnClick = btnResetClick
  end
  object btnQuit: TButton
    Left = 520
    Top = 496
    Width = 97
    Height = 25
    Caption = 'Quit'
    TabOrder = 10
    OnClick = btnQuitClick
  end
end
