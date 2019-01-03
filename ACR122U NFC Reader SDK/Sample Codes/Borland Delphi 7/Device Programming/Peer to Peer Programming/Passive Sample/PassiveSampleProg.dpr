program PassiveSampleProg;

uses
  Forms,
  PassiveSample in 'PassiveSample.pas' {frmPassive};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmPassive, frmPassive);
  Application.Run;
end.
