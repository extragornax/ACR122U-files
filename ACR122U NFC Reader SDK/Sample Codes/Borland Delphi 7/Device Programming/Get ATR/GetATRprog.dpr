program GetATRprog;

uses
  Forms,
  GetATR in 'GetATR.pas' {frmGetATR};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmGetATR, frmGetATR);
  Application.Run;
end.
