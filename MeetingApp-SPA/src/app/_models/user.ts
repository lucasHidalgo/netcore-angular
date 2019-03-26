import { Photo } from './Photo';

export interface User {
    id: number;
    usuarioNombre: string;
    knownAs: string;
    age: number;
    gender: string;
    created: Date;
    lastActivity: Date;
    photoUrl: string;
    city: string;
    country: string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
