# Checkpoint 6 - Programação de ML.NET API

## 📋 Descrição do Projeto
Este projeto foi desenvolvido como parte do Checkpoint 6 da disciplina de Advanced Business Development / DevOps da FIAP. 

Trata-se de uma Web API construída com o framework **ASP.NET Core** que utiliza a biblioteca **ML.NET** para realizar previsões de regressão no setor imobiliário. O objetivo principal é prever o preço de um imóvel utilizando como variáveis de entrada (Features) o seu **Tamanho (em m²)** e a **Quantidade de Quartos**.

O algoritmo de Machine Learning utilizado para o treinamento foi o **Fast Tree Regression**.

---

## 🎥 Demonstração em Vídeo
Assista ao vídeo explicativo de até 5 minutos demonstrando a execução prática da aplicação e a análise de desempenho das duas proporções solicitadas:

* 🔗 [Clique aqui para assistir ao vídeo de demonstração](https://youtu.be/ZCQ9HYX-8Iw)

---

## 👥 Integrantes do Grupo
* **Ivanildo Alfredo da Silva Filho** - RM: 560049
* **Jennyfer Lee** - RM: 561020
* **Leticia Sousa Prado Silva** - RM: 559258

---

## 📊 Dataset (Base de Dados)
A base de dados está localizada obrigatoriamente na pasta `DATA/dados.csv` da raiz do projeto. O dataset conta com **mais de 200 linhas de registros** contendo dados sintéticos de imóveis com ruídos e variações reais de mercado (simulando imóveis mais valorizados, reformas ou depreciações), garantindo uma análise estatística sólida para o modelo.

---

## 🚀 Exemplo de Execução e Análise de Desempenho

A API foi projetada para avaliar dinamicamente o desempenho do modelo alterando a proporção de divisão entre dados de Treino e dados de Teste através de parâmetros na URL.

### 1. Cenário Inicial: Proporção 70/30 (Treino/Teste)
* **Endpoint:** `https://localhost:7225/treinar` (ou utilizando a porta padrão do IIS Express)
* **Métricas Obtidas:**
  * `rSquared`: **0.989774288675256** (Aproximadamente 98.97% de precisão)
  * `rootMeanSquaredError`: **35331.5795117847**

### 2. Cenário Modificado: Proporção 60/40 (Treino/Teste)
* **Endpoint:** `https://localhost:7225/treinar?testFraction=0.4`
* **Métricas Obtidas:**
  * `rSquared`: **0.989352181474945** (Aproximadamente 98.93% de precisão)
  * `rootMeanSquaredError`: **35106.0733028549**

### 🔍 Conclusão da Análise de Desempenho
Com a modificação da porcentagem de treino/teste para a relação **60/40**, **não houve melhora no desempenho geral da precisão**. 

O coeficiente de determinação ($R^2$) sofreu uma leve queda de **0.9897** para **0.9893**. Esse comportamento é matematicamente esperado e totalmente coerente com a teoria de Machine Learning: ao reduzirmos o volume de dados disponíveis para o treinamento da árvore de decisão (de 70% para 60%), o algoritmo *Fast Tree* teve menos insumos para aprender e mapear os ruídos e variações do mercado imobiliário, resultando em uma sutil perda de capacidade preditiva no conjunto de testes.
