program OtherPICCProg;

uses
  Forms,
  OtherPICC in 'OtherPICC.pas' {frmPICC};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmPICC, frmPICC);
  Application.Run;
end.
