import axios from 'axios';
import React, { useState } from 'react';
import { Header } from '../../components/Header/Header';

import './cadastroUsuario.css';

export const Cadastro = (props) => {

  const [nome, setNome] = useState('')
  const [email, setEmail] = useState('')
  const [senha, setSenha] = useState('')
  const [telefone, setTelefone] = useState('')
  const [celular, setCelular] = useState('')
  const [cnpj, setCnpj] = useState('')
  const [razaoSocial, setRazaoSocial] = useState('')
  const [tipoUsuario, setTipoUsuario] = useState()
  const [resposta, setResposta] = useState('')
  const [avisoTelefone, setAvisoTelefone] = useState('')
  const [avisoCelular, setAvisoCelular] = useState('')

  const cadastrarUsuario = (event) => {
    event.preventDefault();

    if (telefone.length < 12) {
      setAvisoTelefone('Número de telefone informado inválido')
    }
    else {
      setAvisoTelefone('')
    }

    if (celular.length < 13) {
      setAvisoCelular('Número de celular informado inválido')
    }
    else {
      setAvisoCelular('')
    }

    axios.post('http://localhost:5000/api/Usuario', {
      nome: nome,
      email: email,
      senha: senha,
      telefone: telefone,
      celular: celular,
      cnpj: cnpj,
      razaoSocial: razaoSocial,
      tipoUsuario: tipoUsuario
    })

      .then(resposta => {
        console.log(resposta)

        if (resposta.status === 201) {
          props.history.push('/login');
        }
      })

      .catch(erro => {
        console.log(erro)
        setResposta("Algo deu errado, favor verificar as informações inseridas")
      })
  }


  return (
    <>
      <Header />
      <main>
        <section className="cadastroo">
          <form className="cadastro-content">
            <div className="titulo">
              <h1>Cadastro</h1>
            </div>
            <div className="form-content">
              <div className="inputs">
                <input
                  type="text"
                  value={nome}
                  placeholder="Nome"
                  onChange={(event) => setNome(event.target.value)}
                />
              </div>
              <div className="inputs">
                <input
                  type="email"
                  value={email}
                  placeholder="Email"
                  onChange={(event) => setEmail(event.target.value)}
                />
              </div>
              <div className="inputs">
                <input
                  type="text"
                  value={senha}
                  placeholder="Senha"
                  onChange={(event) => setSenha(event.target.value)}
                />
              </div>
              <div className="inputs">
                {avisoTelefone === '' ? '' : <p className='respostaRequisicao'>{avisoTelefone}</p>}
                <input
                  // mask="+99 (99) 9999-9999"
                  maxLength={12}
                  type="text"
                  value={telefone}
                  placeholder="Telefone (DDI e DDD obrigatórios)"
                  onChange={(event) => setTelefone(event.target.value)}
                />
              </div>
              <div className="inputs">
                {avisoCelular === '' ? '' : <p className='respostaRequisicao'>{avisoCelular}</p>}
                <input
                  //mask= "+99 (99)99999-9999"
                  maxLength={13}
                  type="text"
                  value={celular}
                  placeholder="Celular (DDI e DDD obrigatórios)"
                  onChange={(event) => setCelular(event.target.value)}
                />
              </div>
              <div className="inputs">
                <input
                  //mask= "99.999.999/9999-99"
                  type="text"
                  value={cnpj}
                  placeholder="CNPJ"
                  onChange={(event) => setCnpj(event.target.value)}
                />
              </div>
              <div className="inputs">
                <input
                  type="text"
                  value={razaoSocial}
                  placeholder="Razão Social"
                  onChange={(event) => setRazaoSocial(event.target.value)}
                />
              </div>
              <div className="inputs">
                <select

                  value={tipoUsuario}
                  onChange={(event) => setTipoUsuario(event.target.value)}>
                  <option>Tipo de Usuario</option>
                  <option value="0">Lojista</option>
                  <option value="1">Fornecedor</option>
                </select>
              </div>
            </div>
            <p className='respostaRequisicao'>{resposta}</p>
            <div className="box-botao">
              <button onClick={cadastrarUsuario} type="submit" className="btn-cadastro">Cadastrar</button>
            </div>
          </form>
        </section>

      </main>
    </>
  )
}


export default Cadastro;