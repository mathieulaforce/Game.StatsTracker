export interface GameServer {
  name: string;
  ip: string;
  mapName: string;
  countryIso2: string;
  ping: number;
  numberOfPlayers: number;
  maxPlayers: number;
  version: string;
  lastOnline: Date;
  isTracking: boolean;
  isOnline: boolean;
  passwordProtected: boolean;
}
