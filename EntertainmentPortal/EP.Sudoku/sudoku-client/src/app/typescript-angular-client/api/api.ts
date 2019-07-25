export * from './auth.service';
import { AuthService } from './auth.service';
export * from './players.service';
import { PlayersService } from './players.service';
export * from './sessions.service';
import { SessionsService } from './sessions.service';
export const APIS = [AuthService, PlayersService, SessionsService];
