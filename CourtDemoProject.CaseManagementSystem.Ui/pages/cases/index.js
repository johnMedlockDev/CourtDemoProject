import styles from '../../styles/pages/cases/Cases.module.scss'
import PropTypes from 'prop-types'
import axios from 'axios'
import Link from 'next/link'
import { useRouter } from 'next/router'

const CasesPage = ({ cases }) => {
	const router = useRouter()

	const handleDelete = async (caseId) => {
		try {
			await axios.delete(`http://api:8080/v1/Cases/${caseId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting case:', error)
		}
	}

	return (
		<div>
			<h1>Cases</h1>
			<Link href="/cases/create"><a>Create New Case</a></Link>
			<ul>
				{cases.map((caseItem) => (
					<li key={caseItem.caseId}>
						<Link href={`/cases/${caseItem.caseId}`}>
							<a>
								<p>Court Name: {caseItem.courtName}</p>
								<p>Case Type: {caseItem.caseType}</p>
							</a>
						</Link>
						<button onClick={() => handleDelete(caseItem.caseId)}>Delete</button>
					</li>
				))}
			</ul>
		</div>
	)
}

export const getServerSideProps = async () => {
	const res = await axios.get('http://api:8080/v1/Cases')
	const cases = res.data // Adjust this according to the API response

	return {
		props: { cases }
	}
}

CasesPage.propTypes = {
	cases: PropTypes.arrayOf(
		PropTypes.shape({
			caseId: PropTypes.string.isRequired,
			courtName: PropTypes.string,
			caseType: PropTypes.number.isRequired
			// Include other properties as required
		})
	).isRequired
}

export default CasesPage
