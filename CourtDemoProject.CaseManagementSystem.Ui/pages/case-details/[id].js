import styles from '../../styles/pages/case-details/Detail.module.scss'
import PropTypes from 'prop-types'
import axios from 'axios'

const CaseDetailPage = ({ caseDetail }) => {
	return (
		<div>
			<h1>Case Detail</h1>
			{caseDetail
				? (
					<div>
						<p>Date: {new Date(caseDetail.caseDetailEntryDateTime).toLocaleDateString()}</p>
						<p>Description: {caseDetail.description}</p>
						<p>Docket Detail: {caseDetail.docketDetail}</p>
						{caseDetail.documentUri && <p>Document: <a href={caseDetail.documentUri}>{caseDetail.documentUri}</a></p>}
					</div>
				)
				: (
					<p>Case detail not found.</p>
				)}
		</div>
	)
}

export const getServerSideProps = async (context) => {
	const { id } = context.params

	try {
		const res = await axios.get(`http://api:8080/v1/CaseDetails/${id}`)
		const caseDetail = res.data // Adjust this according to the API response

		return {
			props: { caseDetail }
		}
	} catch (error) {
		console.error('Error fetching case detail:', error)
		return {
			props: { caseDetail: null }
		}
	}
}

CaseDetailPage.propTypes = {
	caseDetail: PropTypes.shape({
		caseDetailId: PropTypes.string.isRequired,
		caseDetailEntryDateTime: PropTypes.string.isRequired,
		description: PropTypes.string,
		docketDetail: PropTypes.string,
		documentUri: PropTypes.string
	})
}

export default CaseDetailPage
