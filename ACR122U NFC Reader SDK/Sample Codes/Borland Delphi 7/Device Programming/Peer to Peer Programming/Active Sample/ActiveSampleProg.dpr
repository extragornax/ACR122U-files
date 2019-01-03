program ActiveSampleProg;

uses
  Forms,
  ActiveSample in 'ActiveSample.pas' {frmActive};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmActive, frmActive);
  Application.Run;
end.
