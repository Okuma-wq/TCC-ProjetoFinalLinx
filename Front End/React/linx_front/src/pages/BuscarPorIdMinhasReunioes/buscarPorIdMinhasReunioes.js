import axios from 'axios';
import { Component } from 'react';

export default class ReuniaoBuscada extends Component{
    constructor(props){
        super(props);
        this.state = {
            minhasReunioes: {},
            idMinhasReunioes: this.props.location.state.minhasReunioesId,
            idNome: 'e5cd4dde-5a91-4482-85f6-bd62bbec7260',
            local:'',
            ReuniaoData: ''
        }
    }

    buscarPorIdMinhasReunioes = () => {

        let headers = {
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }  
          }

        axios(`http://localhost:5000/api/Reuniao/Minhas/${this.state.idMinhasReunioes}`,headers)

        .then(resposta => {
            this.setState({minhasReunioes: resposta.data})
            console.log(this.state.minhasReunioes)
        }) 

        .catch(erro => console.log(erro))
    }

    atualizarState = async (event) => {
        await this.setState({ local : event.target.value })
    };

    atualizarState1 = async (event) => {
        await this.setState({ ReuniaoData : event.target.value })
    };

    buscaridNome = async (nome) =>
    {
        await this.setState({
            idNome : nome.idMinhasReunioes,
            nome : nome.nome
            
        })
        console.log(this.state.idNome, this.state.nome)
    }

    atualizarNome = (event) =>
    {
        event.preventDefault()

        fetch(`http://localhost:5000/api/Reuniao/Minhas/${this.state.idMinhasReunioes}/Alterar`,
        {   
            // Define o método da requisição ( PUT )
            method : 'PATCH',

            // Define o corpo da requisição especificando o tipo ( JSON )
            body : JSON.stringify({
            dataReuniao : this.state.ReuniaoData,
            local : this.state.local
            
            }),

            // Define o cabeçalho da requisição
            headers : {
                "Content-Type" : "application/json",
                'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })

        .then(this.buscarPorIdMinhasReunioes())        
    }

    componentDidMount(){
        this.buscarPorIdMinhasReunioes();
    }

    render() {
        return(
            <>
              <h3>{this.state.minhasReunioes.tituloAnuncio}</h3>
              <p>{this.state.minhasReunioes.dataReuniao}</p>
              <p>{this.state.minhasReunioes.local}</p>
              <p>{this.state.minhasReunioes.nome}</p>
              <p>{this.state.minhasReunioes.razaoSocial}</p>


              <form onSubmit={this.atualizarNome}>
                  <input type='text' value={this.state.local} onChange={this.atualizarState}></input>
                  <input type='date' value={this.state.ReuniaoData} onChange={this.atualizarState1}></input>
                  <button type='submit'>Editar</button>
              </form>
            </>
        )
    }
}