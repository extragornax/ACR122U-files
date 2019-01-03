program PollingProg;

uses
  Forms,
  Polling in 'Polling.pas' {frmPoll};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmPoll, frmPoll);
  Application.Run;
end.
