export interface Server {
  hostname: string;
  ipAddress: string;
  cheats: boolean;
  version: string;
  miles: boolean;
  adminName: string;
  passwordProtected: boolean;
}

export interface MatchInformation {
  mapName: string;
  timeLeft: string;
  currentRound: number;
  totalRounds: number;
}

export interface OnlinePlayer {
  name: string;
  leader: number;
  goal: number;
  ping: number;
  roe: number;
  kia: number;
  enemy: number;
  honor: number;
}

export interface ScoreBoard {
  onlinePlayers: OnlinePlayer[];
}

export interface GameSession {
  id: string;
  server: Server;
  matchInformation: MatchInformation;
  scoreBoard: ScoreBoard;
}
