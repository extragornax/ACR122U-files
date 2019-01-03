program MiFareProgramming;

uses
  Forms,
  MiFareProg in 'MiFareProg.pas' {frmMifare};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMifare, frmMifare);
  Application.Run;
end.
