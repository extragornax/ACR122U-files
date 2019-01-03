program DeviceProgramming;

uses
  Forms,
  DeviceProg in 'DeviceProg.pas' {frmDevProg};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmDevProg, frmDevProg);
  Application.Run;
end.
