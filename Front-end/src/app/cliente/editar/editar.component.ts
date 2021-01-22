import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControlName, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { NgBrazilValidators } from 'ng-brazil';
import { utilsBr } from 'js-brasil';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';

import { StringUtils } from 'src/app/utils/string-utils';
import { Cliente } from '../models/cliente';
import { Endereco, CepConsulta } from '../models/endereco';
import { ClienteService } from '../services/cliente.service';
import { FormBaseComponent } from 'src/app/base-components/form-base.component';
import { toInteger } from '@ng-bootstrap/ng-bootstrap/util/util';
import { MidiaSocial } from '../models/midiaSocial';
import { Telefone } from '../models/telefone';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html'
})
export class EditarComponent extends FormBaseComponent implements OnInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  errorsEndereco: any[] = [];
  clienteForm: FormGroup;
  enderecoForm: FormGroup;
  telefoneForm: FormGroup;
  midiaSocialForm: FormGroup;

  cliente: Cliente = new Cliente();
  endereco: Endereco = new Endereco();
  telefone: Telefone = new Telefone();
  midiaSocial: MidiaSocial = new MidiaSocial();

  textoDocumento: string = '';

  MASKS = utilsBr.MASKS;
  tipoCliente: number;

  constructor(private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private spinner: NgxSpinnerService) {

    super();
    this.validationMessages = {
      nome: {
        required: 'Informe o Nome',
      },
      cpf: {
        required: 'Informe o CPF',
      },
      rg: {
        required: 'Informe o RG',
      },
      logradouro: {
        required: 'Informe o Logradouro',
      },
      numero: {
        required: 'Informe o Número',
      },
      bairro: {
        required: 'Informe o Bairro',
      },
      cep: {
        required: 'Informe o CEP',
        cep: 'CEP em formato inválido'
      },
      cidade: {
        required: 'Informe a Cidade',
      },
      estado: {
        required: 'Informe o Estado',
      },
      numeroTelefone: {
        required: 'Informe o número de telefone'
      },
      valor: {
        required: 'Informe a rede social'
      }
    };


    super.configurarMensagensValidacaoBase(this.validationMessages);

    this.cliente = this.route.snapshot.data['cliente'];
  }

  ngOnInit() {

    this.spinner.show();

    this.clienteForm = this.fb.group({
      nome: ['', [Validators.required]],
      documento: ['', [Validators.required, NgBrazilValidators.cpf]],
      ativo: ['', [Validators.required]],
      tipocliente: ['', [Validators.required]],

      endereco: this.fb.group({
        logradouro: ['', [Validators.required]],
        numero: ['', [Validators.required]],
        complemento: [''],
        bairro: ['', [Validators.required]],
        cep: ['', [Validators.required, NgBrazilValidators.cep]],
        cidade: ['', [Validators.required]],
        estado: ['', [Validators.required]]
      }),

      telefone: this.fb.group({
        numero: ['', [Validators.required]],
      }),

      midiaSocial: this.fb.group({
        valor: ['', [Validators.required]],
      })

    });

    this.preencherForm();

    setTimeout(() => {
      this.spinner.hide();
    }, 1000);
  }

  preencherForm() {

    this.clienteForm.patchValue({
      id: this.cliente.id,
      nome: this.cliente.nome,
      cpf: this.cliente.cpf,
      rg: this.cliente.rg,
      dataNascimento: this.cliente.dataNascimento,
    });

    this.enderecoForm.patchValue({
      id: this.cliente.enderecos[0].id,
      logradouro: this.cliente.enderecos[0].logradouro,
      numero: this.cliente.enderecos[0].numero,
      complemento: this.cliente.enderecos[0].complemento,
      bairro: this.cliente.enderecos[0].bairro,
      cep: this.cliente.enderecos[0].cep,
      cidade: this.cliente.enderecos[0].cidade,
      estado: this.cliente.enderecos[0].estado
    });

    this.telefoneForm.patchValue({
      id: this.cliente.telefones[0].id,
      numero: this.cliente.telefones[0].numero
    });

    this.midiaSocialForm.patchValue({
      id: this.cliente.midiasSociais[0].id,
      numero: this.cliente.midiasSociais[0].valor
    });
  }

  ngAfterViewInit() {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.clienteForm);
  }

  buscarCep(cep: string) {

    cep = StringUtils.somenteNumeros(cep);
    if (cep.length < 8) return;

    this.clienteService.consultarCep(cep)
      .subscribe(
        cepRetorno => this.preencherEnderecoConsulta(cepRetorno),
        erro => this.errors.push(erro));
  }

  preencherEnderecoConsulta(cepConsulta: CepConsulta) {

    this.enderecoForm.patchValue({
      logradouro: cepConsulta.logradouro,
      bairro: cepConsulta.bairro,
      cep: cepConsulta.cep,
      cidade: cepConsulta.localidade,
      estado: cepConsulta.uf
    });
  }

  editarCliente() {
    if (this.clienteForm.dirty && this.clienteForm.valid) {

      this.cliente = Object.assign({}, this.cliente, this.clienteForm.value);
      this.cliente.cpf = StringUtils.somenteNumeros(this.cliente.cpf);
      this.cliente.rg = StringUtils.somenteNumeros(this.cliente.rg);

      this.clienteService.atualizarCliente(this.cliente)
        .subscribe(
          sucesso => { this.processarSucesso(sucesso) },
          falha => { this.processarFalha(falha) }
        );
    }
  }

  processarSucesso(response: any) {
    this.errors = [];

    let toast = this.toastr.success('Cliente atualizado com sucesso!', 'Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/clientes/listar-todos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  editarEndereco() {
    if (this.enderecoForm.dirty && this.enderecoForm.valid) {

      this.endereco = Object.assign({}, this.endereco, this.enderecoForm.value);

      this.endereco.cep = StringUtils.somenteNumeros(this.endereco.cep);

      this.clienteService.atualizarEndereco(this.endereco)
        .subscribe(
          () => this.processarSucessoEndereco(this.endereco),
          falha => { this.processarFalhaEndereco(falha) }
        );
    }
  }

  processarSucessoEndereco(endereco: Endereco) {
    this.errors = [];

    this.toastr.success('Endereço atualizado com sucesso!', 'Sucesso!');
    this.cliente.enderecos[0] = endereco
    this.modalService.dismissAll();
  }

  processarFalhaEndereco(fail: any) {
    this.errorsEndereco = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  processarSucessoTelefone(telefone: Telefone) {
    this.errors = [];

    this.toastr.success('Endereço atualizado com sucesso!', 'Sucesso!');
    this.cliente.telefones[0] = telefone
    this.modalService.dismissAll();
  }

  processarFalhaTelefone(fail: any) {
    this.errorsEndereco = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  processarSucessoMidiaSocial(midiaSocial: MidiaSocial) {
    this.errors = [];

    this.toastr.success('Midia Social atualizada com sucesso!', 'Sucesso!');
    this.cliente.midiasSociais[0] = midiaSocial
    this.modalService.dismissAll();
  }

  processarFalhaMidiaSocial(fail: any) {
    this.errorsEndereco = fail.error.errors;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  abrirModal(content) {
    this.modalService.open(content);
  }
}
