import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { BaseService } from 'src/app/services/base.service';
import { Cliente } from '../models/cliente';
import { CepConsulta, Endereco } from '../models/endereco';
import { Telefone } from "../models/telefone";
import { MidiaSocial } from "../models/midiaSocial";

@Injectable()
export class ClienteService extends BaseService {

    Cliente: Cliente = new Cliente();

    constructor(private http: HttpClient) { super() }

    obterTodos(): Observable<Cliente[]> {
        return this.http
            .get<Cliente[]>(this.UrlServiceV1 + "clientes")
            .pipe(catchError(super.serviceError));
    }

    obterPorId(id: string): Observable<Cliente> {
        return this.http
            .get<Cliente>(this.UrlServiceV1 + "clientes/" + id, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }

    novoCliente(Cliente: Cliente): Observable<Cliente> {
        return this.http
            .post(this.UrlServiceV1 + "clientes", Cliente, this.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarCliente(Cliente: Cliente): Observable<Cliente> {
        return this.http
            .put(this.UrlServiceV1 + "clientes/" + Cliente.id, Cliente, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    excluirCliente(id: string): Observable<Cliente> {
        return this.http
            .delete(this.UrlServiceV1 + "clientes/" + id, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarEndereco(endereco: Endereco): Observable<Endereco> {
        return this.http
            .put(this.UrlServiceV1 + "clientes/endereco/" + endereco.id, endereco, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarTelefone(telefone: Telefone): Observable<Telefone> {
        return this.http
            .put(this.UrlServiceV1 + "clientes/telefone/" + telefone.id, telefone, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    atualizarMidiaSocial(midiaSocial: MidiaSocial): Observable<MidiaSocial> {
        return this.http
            .put(this.UrlServiceV1 + "clientes/midiaSocial/" + midiaSocial.id, midiaSocial, super.ObterAuthHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));
    }

    consultarCep(cep: string): Observable<CepConsulta> {
        return this.http
            .get<CepConsulta>(`https://viacep.com.br/ws/${cep}/json/`)
            .pipe(catchError(super.serviceError))
    }
}
