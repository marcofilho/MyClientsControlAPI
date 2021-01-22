import { Endereco } from './endereco';
import { MidiaSocial } from './midiaSocial';
import { Telefone } from './telefone';

export class Cliente {
    id: string;
    nome: string;
    cpf: string;
    rg: string;
    dataNascimento: Date;
    enderecos: Endereco[];
    telefones: Telefone[];
    midiasSociais: MidiaSocial[];
}

