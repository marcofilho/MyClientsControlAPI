import { Component } from '@angular/core';
import { Cliente } from '../models/cliente';

import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService as ClienteService } from '../services/cliente.service';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-excluir',
  templateUrl: './excluir.component.html'
})
export class ExcluirComponent {

  cliente: Cliente = new Cliente();
  enderecoMap;
  errors: any[] = [];

  constructor(
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private sanitizer: DomSanitizer) {

    this.cliente = this.route.snapshot.data['cliente'];
  }

  excluirEvento() {
    this.clienteService.excluirCliente(this.cliente.id)
      .subscribe(
        cliente => { this.sucessoExclusao(cliente) },
        error => { this.falha(error) }
      );
  }

  sucessoExclusao(evento: any) {

    const toast = this.toastr.success('Cliente excluido com Sucesso!', 'Good bye :D');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/clientes/listar-todos']);
      });
    }
  }

  falha(fail) {
    this.errors = fail.error.errors;
    this.toastr.error('Houve um erro no processamento!', 'Ops! :(');
  }
}
