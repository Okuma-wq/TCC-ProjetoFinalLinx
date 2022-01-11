// import axios from 'axios';
import { Component } from 'react';

import './minhasReunioes.css'
import { Header } from '../../components/Header/Header';

export default class minhasReunioes extends Component {
    constructor(props) {
        super(props);
        this.state = {
            listarMinhasReunioes: [],
            minhasReunioesId: 'vazio'
        }
    }

    buscarMinhasReunioes = () => {

            fetch('http://localhost:5000/api/Reuniao/Minhas', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('usuario-login')
            }
        })

            .then(resposta => {
                if (resposta.status !== 200) {
                    throw Error();
                }

                return resposta.json();
            })

            .then(resposta => this.setState({ listarMinhasReunioes: resposta }))

            .catch(erro => console.log(erro))
    };

    buscarPorIdMinhasReunioes(id){
        this.props.history.push({
            pathname: `/minhas/${id}`,
            state: {minhasReunioesId: id}
        })
    }

    componentDidMount() {
        this.buscarMinhasReunioes();
    }

    render() {
        return (
            <>
                <Header />
                <main>
                    <section className='reunioes-content'>
                        <div className='cabecalhoReunioes'>
                            <h1>Minhas Reuniões</h1>
                            <img src='https://cdn.discordapp.com/attachments/892018545653841930/920856102877417502/image_5.png'></img>
                        </div>
                        <div className='areaCardsReuniao'>
                            {
                                this.state.listarMinhasReunioes.map((reuniao) => {
                                    return (
                                        <div className='CardReuniao'>
                                            <div className='CardReuniaoContent'>
                                                <div>
                                                    <p>{reuniao.tituloAnuncio}</p>
                                                    <p>{reuniao.nome}</p>
                                                </div>
                                                <p>{Intl.DateTimeFormat("pt-BR", {
                                                year: 'numeric', month: 'numeric', day: 'numeric',
                                                hour: 'numeric', minute: 'numeric',
                                                hour12: false
                                            }).format(new Date(reuniao.dataReuniao))}</p>
                                            </div>
                                        </div>
                                    )
                                })
                            }
                        </div>
                    </section>
                </main>
            </>
        )
    }
}