import { Component } from '@angular/core';
import { Cliente } from '../models/cliente';
import { DomSanitizer } from '@angular/platform-browser';

import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detalhes',
  templateUrl: './detalhes.component.html'
})
export class DetalhesComponent {

  cliente: Cliente = new Cliente();
  enderecoMap;

  constructor(
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer) {

      this.cliente = this.route.snapshot.data['fornecedor'];
      this.enderecoMap = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.google.com/maps/embed/v1/place?q=" + this.EnderecoCompleto() + "&key=AIzaSyAP0WKpL7uTRHGKWyakgQXbW6FUhrrA5pE");
  }

  public EnderecoCompleto(): string {
    return this.cliente.enderecos[0].logradouro + ", " + this.cliente.enderecos[0].numero + " - " + this.cliente.enderecos[0].bairro + ", " + this.cliente.enderecos[0].cidade + " - " + this.cliente.enderecos[0].estado;
  }
}
