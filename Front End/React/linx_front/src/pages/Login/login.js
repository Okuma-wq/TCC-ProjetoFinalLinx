import React, { Component } from "react";

import axios from 'axios';

import { Link } from "react-router-dom";

import './login.css'
import { Header } from "../../components/Header/Header";

class login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: '',
      senha: '',
      erroMensagem: '',
      isLoading: false
    }
  };

  atualizaStateCampo = (campo) => {
    this.setState({ [campo.target.name]: campo.target.value })
  }

  efetuarLogin = (event) => {
    event.preventDefault();

    this.setState({ erroMensagem: '', isLoading: true });

      axios.post('http://localhost:5000/api/Login',{

      email: this.state.email,
      senha: this.state.senha
    })

      .then(resposta => {
        if (resposta.status === 200) {
          localStorage.setItem('usuario-login', resposta.data)

          console.log('meu token é:' + resposta.data)
          this.setState({ isLoading: false })

          this.props.history.push('/');
        }

      })

      .catch(() => {
        this.setState({ errorMensagem: 'E-mail ou senha incorreto!', isLoading: false })
      })
  }

  render() {
    return (
      <>
        <Header />
        <main>
          <section className="login-content">
            <form className="form-content-login">
              <div className="head-login">
                <h1>Bem-vindo de volta!</h1>
                <p>Estamos muito alegres com sua presença!</p>
                <div className="horizontal-line" />
              </div>
              <div className="inputs-login">
                <input
                  type="email"
                  name="email"
                  value={this.state.email}
                  placeholder="E-mail"
                  onChange={this.atualizaStateCampo}
                />
              </div>
              <div className="inputs-login">
                <input
                  type="password"
                  name="senha"
                  value={this.state.senha}
                  placeholder="Senha"
                  onChange={this.atualizaStateCampo}
                />
              </div>
              <div>
                <p style={{ color: 'red', fontSize: "15px" }}>{this.state.errorMensagem}</p>
              </div>
              <div className="btn-login-content">
                {
                  this.state.isLoading === true &&
                  <button type="submit" disabled className="btn-loading">Loading...</button>
                }
                {
                  this.state.isLoading === false &&
                  <button type="submit" disabled={this.state.email === '' || this.state.section === '' ? 'none' : ''} className="btn-login" onClick={this.efetuarLogin}>Entrar</button>
                }
              </div>
              <p className="card-link">Nãe é cadastrado ? Então <span><Link className="card-link-link" to="/cadastro">cadastra-se</Link></span></p>
            </form>
          </section>
        </main>
      </>
    )
  }
}

export default login;